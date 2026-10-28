using System.Buffers;
using System.Collections.Concurrent;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Protocol;
using SabEnergy.Application.Modules.Interfaces.Mqtt;

namespace SabEnergy.Infrastructure.Mqtt;

public class MqttService : BackgroundService, IMqttService
{
    private readonly MqttSettings _settings;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<MqttService> _logger;
    private IMqttClient? _mqttClient;
    private MqttClientOptions? _options;

    private readonly ConcurrentDictionary<string, TaskCompletionSource<string>> _pendingResponses = new();

    public MqttService(
        IOptions<MqttSettings> settings,
        IServiceScopeFactory scopeFactory,
        ILogger<MqttService> logger)
    {
        _settings = settings.Value;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    public bool IsConnected => _mqttClient?.IsConnected ?? false;

    public async Task PublishAsync(string topic, string payload, bool retain = false, int qos = 1, CancellationToken cancellationToken = default)
    {
        if (_mqttClient == null || !_mqttClient.IsConnected)
        {
            _logger.LogWarning("MQTT client is not connected. Attempting reconnection before publishing...");
            if (_mqttClient != null && _options != null)
            {
                await _mqttClient.ConnectAsync(_options, cancellationToken);
            }
            else
            {
                throw new InvalidOperationException("MQTT client has not been initialized yet.");
            }
        }

        var message = new MqttApplicationMessageBuilder()
            .WithTopic(topic)
            .WithPayload(Encoding.UTF8.GetBytes(payload))
            .WithQualityOfServiceLevel((MqttQualityOfServiceLevel)qos)
            .WithRetainFlag(retain)
            .Build();

        await _mqttClient.PublishAsync(message, cancellationToken);
        _logger.LogInformation("Published message to Topic: {Topic}", topic);
    }

    public async Task<string?> PublishAndWaitForResponseAsync(
        string publishTopic,
        string payload,
        string expectedResponseTopicPattern,
        TimeSpan timeout,
        CancellationToken cancellationToken = default)
    {
        var tcs = new TaskCompletionSource<string>(TaskCreationOptions.RunContinuationsAsynchronously);
        var matchKey = expectedResponseTopicPattern.ToLowerInvariant();

        _pendingResponses[matchKey] = tcs;

        try
        {
            await PublishAsync(publishTopic, payload, retain: false, qos: 1, cancellationToken);

            using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            cts.CancelAfter(timeout);

            using (cts.Token.Register(() => tcs.TrySetCanceled()))
            {
                return await tcs.Task;
            }
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("Timed out or cancelled waiting for device response on topic matching {Topic}", expectedResponseTopicPattern);
            return null;
        }
        finally
        {
            _pendingResponses.TryRemove(matchKey, out _);
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new MqttClientFactory();
        _mqttClient = factory.CreateMqttClient();

        var clientId = string.IsNullOrWhiteSpace(_settings.ClientId) 
            ? $"SabEnergy_{Guid.NewGuid():N}" 
            : $"{_settings.ClientId}_{Environment.ProcessId}";

        var builder = new MqttClientOptionsBuilder()
            .WithTcpServer(_settings.Host, _settings.Port)
            .WithClientId(clientId)
            .WithCleanSession();

        if (!string.IsNullOrWhiteSpace(_settings.Username))
        {
            builder.WithCredentials(_settings.Username, _settings.Password);
        }

        if (_settings.UseTls)
        {
            builder.WithTlsOptions(o => o.UseTls());
        }

        _options = builder.Build();

        _mqttClient.ApplicationMessageReceivedAsync += async e =>
        {
            try
            {
                var topic = e.ApplicationMessage.Topic;
                var payloadBytes = e.ApplicationMessage.Payload.ToArray();
                var payload = Encoding.UTF8.GetString(payloadBytes);

                _logger.LogInformation("Received MQTT message on Topic: {Topic}", topic);

                // Check if any request is waiting for a response on this exact topic or wildcard match
                var topicLower = topic.ToLowerInvariant();
                foreach (var pair in _pendingResponses)
                {
                    var prefix = pair.Key.Replace("/+", "").Replace("/#", "").TrimEnd('/');
                    if (topicLower.Equals(pair.Key) || topicLower.StartsWith(prefix) || TopicMatches(pair.Key, topicLower))
                    {
                        _logger.LogInformation("Matched pending response for key '{Key}' with received topic '{Topic}'", pair.Key, topic);
                        pair.Value.TrySetResult(payload);
                    }
                }

                using var scope = _scopeFactory.CreateScope();
                var handler = scope.ServiceProvider.GetRequiredService<ITelemetryHandler>();
                await handler.HandleMessageAsync(topic, payload, stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling incoming MQTT message.");
            }
        };

        _mqttClient.DisconnectedAsync += async e =>
        {
            _logger.LogWarning("Disconnected from Mosquitto MQTT broker. Reason: {Reason}. Will retry...", e.Reason);
            while (!stoppingToken.IsCancellationRequested && !_mqttClient.IsConnected)
            {
                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                    await _mqttClient.ConnectAsync(_options, stoppingToken);
                    _logger.LogInformation("Successfully reconnected to Mosquitto broker.");
                    await ResubscribeTopicsAsync(stoppingToken);
                    break;
                }
                catch
                {
                    _logger.LogWarning("Reconnection failed, retrying in 5 seconds...");
                }
            }
        };

        try
        {
            _logger.LogInformation("Connecting to Mosquitto broker at {Host}:{Port}...", _settings.Host, _settings.Port);
            await _mqttClient.ConnectAsync(_options, stoppingToken);
            _logger.LogInformation("Connected to Mosquitto broker successfully.");

            await ResubscribeTopicsAsync(stoppingToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Initial connection to Mosquitto broker failed. Reconnect handler will manage retry.");
        }

        // Keep service alive while application runs
        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    private async Task ResubscribeTopicsAsync(CancellationToken cancellationToken)
    {
        if (_mqttClient == null || !_mqttClient.IsConnected) return;

        foreach (var topic in _settings.SubscribedTopics)
        {
            var filter = new MqttTopicFilterBuilder()
                .WithTopic(topic)
                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                .Build();

            await _mqttClient.SubscribeAsync(filter, cancellationToken);
            _logger.LogInformation("Subscribed to MQTT topic: {Topic}", topic);
        }
    }

    private static bool TopicMatches(string filter, string topic)
    {
        var filterSegments = filter.Split('/');
        var topicSegments = topic.Split('/');

        for (int i = 0; i < filterSegments.Length; i++)
        {
            if (filterSegments[i] == "#") return true;
            if (i >= topicSegments.Length) return false;
            if (filterSegments[i] == "+") continue;
            if (!string.Equals(filterSegments[i], topicSegments[i], StringComparison.OrdinalIgnoreCase)) return false;
        }

        return filterSegments.Length == topicSegments.Length;
    }
}
