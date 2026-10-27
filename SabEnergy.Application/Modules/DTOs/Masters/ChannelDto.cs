namespace SabEnergy.Application.Modules.DTOs.Masters;

public class ChannelDto
{
    public int Id { get; set; }
    public string ChannelName { get; set; } = string.Empty;
    public int GatewayId { get; set; }
    public string GatewayDeviceName { get; set; } = string.Empty;
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
}

public class CreateChannelDto
{
    public string ChannelName { get; set; } = string.Empty;
    public int GatewayId { get; set; }
}

public class UpdateChannelDto
{
    public string ChannelName { get; set; } = string.Empty;
    public int GatewayId { get; set; }
    public bool IsActive { get; set; } = true;
}

