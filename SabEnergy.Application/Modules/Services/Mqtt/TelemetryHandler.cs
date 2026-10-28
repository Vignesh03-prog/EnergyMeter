using System.Text.Json;
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

            int? slaveId = null;
            try
            {
                using var doc = JsonDocument.Parse(payload);
                if (doc.RootElement.TryGetProperty("slave_id", out var slaveElem) && slaveElem.TryGetInt32(out var parsedSlaveId))
                {
                    slaveId = parsedSlaveId;
                }
            }
            catch
            {
                // If payload is not valid JSON, proceed with null slaveId
            }

            // Store the latest live telemetry reading in cache (by device and slave_id)
            _telemetryStore.Update(deviceId, payload, slaveId);
        }

        return Task.CompletedTask;
    }
}
