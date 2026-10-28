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

            try
            {
                using var doc = JsonDocument.Parse(payload);
                var foundSlaves = ExtractSlaves(doc.RootElement);

                if (foundSlaves.Count > 0)
                {
                    // Update cache for each specific slave discovered
                    foreach (var (slaveId, slaveJson) in foundSlaves)
                    {
                        _telemetryStore.Update(deviceId, slaveJson, slaveId);
                    }
                    // Also maintain the general device latest payload
                    _telemetryStore.Update(deviceId, payload, null);
                }
                else
                {
                    _telemetryStore.Update(deviceId, payload, null);
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to parse JSON telemetry payload for device '{DeviceId}'. Storing as raw device payload.", deviceId);
                _telemetryStore.Update(deviceId, payload, null);
            }
        }

        return Task.CompletedTask;
    }

    private static List<(int SlaveId, string Json)> ExtractSlaves(JsonElement root)
    {
        var list = new List<(int, string)>();

        if (root.ValueKind == JsonValueKind.Object)
        {
            if (TryGetSlaveId(root, out var sId))
            {
                list.Add((sId, root.GetRawText()));
                return list;
            }

            // Check if there is an array property such as "meters", "data", "telemetry", etc.
            foreach (var prop in root.EnumerateObject())
            {
                if (prop.Value.ValueKind == JsonValueKind.Array)
                {
                    foreach (var item in prop.Value.EnumerateArray())
                    {
                        if (item.ValueKind == JsonValueKind.Object && TryGetSlaveId(item, out var childId))
                        {
                            list.Add((childId, item.GetRawText()));
                        }
                    }
                }
            }
        }
        else if (root.ValueKind == JsonValueKind.Array)
        {
            foreach (var item in root.EnumerateArray())
            {
                if (item.ValueKind == JsonValueKind.Object && TryGetSlaveId(item, out var childId))
                {
                    list.Add((childId, item.GetRawText()));
                }
            }
        }

        return list;
    }

    private static bool TryGetSlaveId(JsonElement element, out int slaveId)
    {
        slaveId = 0;
        if (element.TryGetProperty("slave_id", out var prop) ||
            element.TryGetProperty("slaveId", out prop) ||
            element.TryGetProperty("slave", out prop))
        {
            if (prop.ValueKind == JsonValueKind.Number && prop.TryGetInt32(out slaveId))
                return true;
            if (prop.ValueKind == JsonValueKind.String && int.TryParse(prop.GetString(), out slaveId))
                return true;
        }
        return false;
    }
}
