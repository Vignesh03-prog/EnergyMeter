namespace SabEnergy.Application.Modules.Interfaces.Mqtt;

public interface ILatestTelemetryStore
{
    void Update(string deviceId, string payload, int? slaveId = null);
    string? GetLatest(string deviceId, int? slaveId = null);
    Dictionary<int, string> GetLatestBySlaveIds(string deviceId, IEnumerable<int> slaveIds);
    Dictionary<int, string> GetAllSlaves(string deviceId);
}
