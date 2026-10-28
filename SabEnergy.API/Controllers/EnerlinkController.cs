using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using SabEnergy.Application.Modules.DTOs.Enerlink;
using SabEnergy.Application.Modules.Interfaces.Mqtt;

namespace SabEnergy.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnerlinkController : BaseApiController
{
    private readonly IMqttService _mqttService;
    private readonly ILatestTelemetryStore _telemetryStore;
    private readonly ILogger<EnerlinkController> _logger;

    public EnerlinkController(
        IMqttService mqttService,
        ILatestTelemetryStore telemetryStore,
        ILogger<EnerlinkController> logger)
    {
        _mqttService = mqttService;
        _telemetryStore = telemetryStore;
        _logger = logger;
    }


    [HttpPost("devices/{deviceId}/cmd")]
    public async Task<IActionResult> SendDeviceCommand(
        string deviceId,
        [FromBody] EnerlinkCommandDto command,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(deviceId))
        {
            return BadRequestResponse("Device ID is required.");
        }

        var topic = $"enerlink/device/{deviceId}/cmd";
        var payload = JsonSerializer.Serialize(command, new JsonSerializerOptions
        {
            WriteIndented = false
        });

        try
        {
            await _mqttService.PublishAsync(topic, payload, retain: false, qos: 1, cancellationToken);
            _logger.LogInformation("Dispatched command '{Cmd}' to topic '{Topic}'", command.Cmd, topic);

            return OkResponse(new
            {
                Device = deviceId,
                Topic = topic,
                Command = command.Cmd,
                DispatchedAt = DateTime.UtcNow
            }, "Command dispatched successfully to device.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to publish command to MQTT broker on topic {Topic}", topic);
            return ErrorResponse($"Failed to send command to MQTT broker: {ex.Message}");
        }
    }

    /// <summary>
    /// Tests connection with an Enerlink IoT device and returns YES or NO.
    /// Only takes the command JSON body.
    /// </summary>
    /// <param name="command">Connection test payload (profiles & channels)</param>
    [HttpPost("test-connection")]
    public async Task<IActionResult> TestConnection(
        [FromBody] EnerlinkCommandDto command,
        CancellationToken cancellationToken = default)
    {
        // Resolve Device ID: from payload device_id, or fallback to default
        var targetDeviceId = !string.IsNullOrWhiteSpace(command.DeviceId)
            ? command.DeviceId
            : "441BF6D37C80";

        // Ensure command is set to test_connection
        command.Cmd = "test_connection";

        var publishTopic = $"enerlink/device/{targetDeviceId}/cmd";
        var responseTopicPattern = $"enerlink/device/{targetDeviceId}/+";

        var payload = JsonSerializer.Serialize(command, new JsonSerializerOptions
        {
            WriteIndented = false
        });

        try
        {
            // Give 12 seconds for the physical Modbus RS-485 scan to respond
            const int defaultTimeoutSeconds = 12;
            var responseJson = await _mqttService.PublishAndWaitForResponseAsync(
                publishTopic,
                payload,
                responseTopicPattern,
                TimeSpan.FromSeconds(defaultTimeoutSeconds),
                cancellationToken);

            var isConnected = !string.IsNullOrEmpty(responseJson);

            // If responseJson was not captured during test_connection wait, fallback to latest received telemetry
            if (!isConnected)
            {
                var cached = _telemetryStore.GetLatest(targetDeviceId);
                if (!string.IsNullOrEmpty(cached))
                {
                    isConnected = true;
                    responseJson = cached;
                }
            }

            return OkResponse(new
            {
                Connected = isConnected ? "YES" : "NO",
                IsConnected = isConnected,
                DeviceId = targetDeviceId,
                Response = responseJson
            }, isConnected ? "Device connected successfully." : "Device did not respond. Connection test failed.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error testing connection for device {DeviceId}", targetDeviceId);
            return ErrorResponse($"Error during connection test: {ex.Message}");
        }
    }

    /// <summary>
    /// Gets the latest real-time live telemetry received from an Enerlink IoT device
    /// </summary>
    /// <param name="deviceId">Device ID (defaults to 441BF6D37C80)</param>
    [HttpGet("devices/{deviceId}/telemetry/latest")]
    public IActionResult GetLatestTelemetry(string deviceId = "441BF6D37C80")
    {
        var rawJson = _telemetryStore.GetLatest(deviceId);
        if (string.IsNullOrEmpty(rawJson))
        {
            return NotFoundResponse($"No telemetry data received yet for device '{deviceId}'.");
        }

        using var doc = JsonDocument.Parse(rawJson);
        return OkResponse(doc.RootElement.Clone(), "Latest live telemetry retrieved.");
    }

    /// <summary>
    /// Checks the current MQTT Broker connection status
    /// </summary>
    [HttpGet("broker/status")]
    public IActionResult GetBrokerStatus()
    {
        return OkResponse(new
        {
            IsConnected = _mqttService.IsConnected
        }, "MQTT Broker status retrieved.");
    }
}
