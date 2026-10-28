namespace SabEnergy.Infrastructure.Mqtt;

public class MqttSettings
{
    public const string SectionName = "MqttSettings";

    public string Host { get; set; } = "localhost";
    public int Port { get; set; } = 1883;
    public string ClientId { get; set; } = "SabEnergy_Backend";
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool UseTls { get; set; } = false;
    public List<string> SubscribedTopics { get; set; } = new()
    {
        "enerlink/device/+/telemetry",
        "enerlink/device/+/response",
        "enerlink/device/+/status"
    };
}
