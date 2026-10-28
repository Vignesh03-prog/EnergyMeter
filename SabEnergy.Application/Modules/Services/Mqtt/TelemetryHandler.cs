using Microsoft.Extensions.Logging;
using SabEnergy.Application.Modules.Interfaces.Mqtt;

namespace SabEnergy.Application.Modules.Services.Mqtt;

public class TelemetryHandler : ITelemetryHandler
{
    private readonly ILatestTelemetryStore _telemetryStore;
    private readonly ILogger<TelemetryHandler> _logger;

    public TelemetryHandler(ILatestTelemetryStore telemetryStore, ILogger<TelemetryHandler> logger)
    {
        _telemetryStore = telemetryStore;
        _logger = logger;
    }

    public Task HandleMessageAsync(string topic, string payload, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Processing MQTT message on Topic: {Topic}. Payload: {Payload}", topic, payload);

        var segments = topic.Split('/');
        if (segments.Length >= 4 && segments[0].Equals("enerlink", StringComparison.OrdinalIgnoreCase))
        {
            var deviceId = segments[2];
            var action = segments[3];

            _logger.LogInformation("Extracted IoT Device ID: {DeviceId}, Action: {Action}", deviceId, action);

            // Store the latest live telemetry reading in cache
            _telemetryStore.Update(deviceId, payload);
        }

        return Task.CompletedTask;
    }
}
