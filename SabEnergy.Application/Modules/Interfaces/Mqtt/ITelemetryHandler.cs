namespace SabEnergy.Application.Modules.Interfaces.Mqtt;

public interface ITelemetryHandler
{
    Task HandleMessageAsync(string topic, string payload, CancellationToken cancellationToken = default);
}
