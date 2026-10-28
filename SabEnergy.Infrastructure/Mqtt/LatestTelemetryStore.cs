using System.Collections.Concurrent;
using SabEnergy.Application.Modules.Interfaces.Mqtt;

namespace SabEnergy.Infrastructure.Mqtt;

public class LatestTelemetryStore : ILatestTelemetryStore
{
    private readonly ConcurrentDictionary<string, string> _store = new(StringComparer.OrdinalIgnoreCase);

    public void Update(string deviceId, string payload)
    {
        _store[deviceId] = payload;
    }

    public string? GetLatest(string deviceId)
    {
        _store.TryGetValue(deviceId, out var payload);
        return payload;
    }
}
