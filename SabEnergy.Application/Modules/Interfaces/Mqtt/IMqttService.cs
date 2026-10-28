namespace SabEnergy.Application.Modules.Interfaces.Mqtt;

public interface IMqttService
{
    Task PublishAsync(string topic, string payload, bool retain = false, int qos = 1, CancellationToken cancellationToken = default);
    Task<string?> PublishAndWaitForResponseAsync(string publishTopic, string payload, string expectedResponseTopicPattern, TimeSpan timeout, CancellationToken cancellationToken = default);
    bool IsConnected { get; }
}
