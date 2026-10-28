using System.Collections.Concurrent;
using System.Text.Json;
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
        var idList = slaveIds.Distinct().ToList();

        foreach (var id in idList)
        {
            if (_store.TryGetValue($"{deviceId}:{id}", out var payload))
            {
                result[id] = payload;
            }
        }

        // Fallback: If some requested slaves were not cached individually, check general device payload
        if (result.Count < idList.Count && _store.TryGetValue(deviceId, out var generalPayload) && !string.IsNullOrWhiteSpace(generalPayload))
        {
            try
            {
                using var doc = JsonDocument.Parse(generalPayload);
                var missingIds = idList.Where(x => !result.ContainsKey(x)).ToHashSet();
                ExtractMatchingSlaves(doc.RootElement, missingIds, result);
            }
            catch
            {
                // Ignore fallback parsing errors
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

        // Fallback: If no individually prefixed slaves, try extracting all from general payload
        if (result.Count == 0 && _store.TryGetValue(deviceId, out var generalPayload) && !string.IsNullOrWhiteSpace(generalPayload))
        {
            try
            {
                using var doc = JsonDocument.Parse(generalPayload);
                ExtractAllSlaves(doc.RootElement, result);
            }
            catch
            {
                // Ignore fallback parsing errors
            }
        }

        return result;
    }

    private static void ExtractMatchingSlaves(JsonElement element, HashSet<int> requestedIds, Dictionary<int, string> output)
    {
        if (element.ValueKind == JsonValueKind.Object)
        {
            if (TryGetSlaveId(element, out var sId) && requestedIds.Contains(sId))
            {
                output[sId] = element.GetRawText();
            }

            foreach (var prop in element.EnumerateObject())
            {
                if (prop.Value.ValueKind == JsonValueKind.Array)
                {
                    foreach (var item in prop.Value.EnumerateArray())
                    {
                        if (item.ValueKind == JsonValueKind.Object && TryGetSlaveId(item, out var childId) && requestedIds.Contains(childId))
                        {
                            output[childId] = item.GetRawText();
                        }
                    }
                }
            }
        }
        else if (element.ValueKind == JsonValueKind.Array)
        {
            foreach (var item in element.EnumerateArray())
            {
                if (item.ValueKind == JsonValueKind.Object && TryGetSlaveId(item, out var childId) && requestedIds.Contains(childId))
                {
                    output[childId] = item.GetRawText();
                }
            }
        }
    }

    private static void ExtractAllSlaves(JsonElement element, Dictionary<int, string> output)
    {
        if (element.ValueKind == JsonValueKind.Object)
        {
            if (TryGetSlaveId(element, out var sId))
            {
                output[sId] = element.GetRawText();
            }

            foreach (var prop in element.EnumerateObject())
            {
                if (prop.Value.ValueKind == JsonValueKind.Array)
                {
                    foreach (var item in prop.Value.EnumerateArray())
                    {
                        if (item.ValueKind == JsonValueKind.Object && TryGetSlaveId(item, out var childId))
                        {
                            output[childId] = item.GetRawText();
                        }
                    }
                }
            }
        }
        else if (element.ValueKind == JsonValueKind.Array)
        {
            foreach (var item in element.EnumerateArray())
            {
                if (item.ValueKind == JsonValueKind.Object && TryGetSlaveId(item, out var childId))
                {
                    output[childId] = item.GetRawText();
                }
            }
        }
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
