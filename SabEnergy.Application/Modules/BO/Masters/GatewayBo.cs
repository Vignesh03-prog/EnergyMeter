namespace SabEnergy.Application.Modules.BO.Masters;

public class GatewayBo
{
    public int Id { get; set; }
    public string DeviceName { get; set; } = string.Empty;
    public string SerialNo { get; set; } = string.Empty;
    public int GatewayMakeId { get; set; }
    public string GatewayMakeName { get; set; } = string.Empty;
    public int? GatewayModelId { get; set; }
    public string? GatewayModelName { get; set; }
    public string IpAddress { get; set; } = string.Empty;
    public string MacAddress { get; set; } = string.Empty;
    public int ChannelCount { get; set; }
    public string? FirmwareVersion { get; set; }
    public int KeepaliveTime { get; set; }
    public string Wifi_SSID { get; set; } = string.Empty;
    public int? Wifi_Port { get; set; }

    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
}
