using SabEnergy.Domain.Common;
using SabEnergy.Domain.Modules.Masters.Model;

namespace SabEnergy.Domain.Modules.Masters;

public sealed class Gateway : BaseEntity
{
    public string DeviceName { get; set; } = string.Empty;
    public string SerialNo { get; set; } = string.Empty;
    public int GatewayMakeId { get; set; }
    public int? GatewayModelId { get; set; }
    public string IpAddress { get; set; } = string.Empty;
    public string MacAddress { get; set; } = string.Empty;
    public int ChannelCount { get; set; }
    public string? FirmwareVersion { get; set; } = string.Empty;
    public int KeepaliveTime { get; set; }
    public string Wifi_SSID { get; set; } = string.Empty;
    public int? Wifi_Port { get; set; }

    public GatewayMake? GatewayMake { get; set; }
    public GatewayModel? GatewayModel { get; set; }
}