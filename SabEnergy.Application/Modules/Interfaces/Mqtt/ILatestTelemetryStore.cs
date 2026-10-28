namespace SabEnergy.Application.Modules.Interfaces.Mqtt;

public interface ILatestTelemetryStore
{
    void Update(string deviceId, string payload);
    string? GetLatest(string deviceId);
}
