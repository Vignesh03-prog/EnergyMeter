using System.Collections.Concurrent;
using SabEnergy.Application.Modules.Interfaces.Mqtt;

namespace SabEnergy.Infrastructure.Mqtt;

public class LatestTelemetryStore : ILatestTelemetryStore
{
    private readonly ConcurrentDictionary<string, string> _store = new(StringComparer.OrdinalIgnoreCase);

    public void Update(string deviceId, string payload, int? slaveId = null)
    {
        // Store as general latest for this device
        _store[deviceId] = payload;

        // Also store specifically under deviceId:slaveId if slaveId is provided
        if (slaveId.HasValue)
        {
            _store[$"{deviceId}:{slaveId.Value}"] = payload;
        }
    }

    public string? GetLatest(string deviceId, int? slaveId = null)
    {
        if (slaveId.HasValue)
        {
            if (_store.TryGetValue($"{deviceId}:{slaveId.Value}", out var slavePayload))
            {
                return slavePayload;
            }
        }

        _store.TryGetValue(deviceId, out var payload);
        return payload;
    }

    public Dictionary<int, string> GetLatestBySlaveIds(string deviceId, IEnumerable<int> slaveIds)
    {
        var result = new Dictionary<int, string>();
        foreach (var id in slaveIds)
        {
            if (_store.TryGetValue($"{deviceId}:{id}", out var payload))
            {
                result[id] = payload;
            }
        }
        return result;
    }

    public Dictionary<int, string> GetAllSlaves(string deviceId)
    {
        var result = new Dictionary<int, string>();
        var prefix = $"{deviceId}:";

        foreach (var kvp in _store)
        {
            if (kvp.Key.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            {
                var suffix = kvp.Key[prefix.Length..];
                if (int.TryParse(suffix, out var slaveId))
                {
                    result[slaveId] = kvp.Value;
                }
            }
        }

        return result;
    }
}
