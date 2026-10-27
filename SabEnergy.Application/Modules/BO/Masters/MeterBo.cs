namespace SabEnergy.Application.Modules.BO.Masters;

public class MeterBo
{
    public int Id { get; set; }
    public string SerialNo { get; set; } = string.Empty;
    public string CustomName { get; set; } = string.Empty;
    public int ChannelId { get; set; }
    public int SlaveId { get; set; }
    public int DeviceProfileId { get; set; }
    public int MeterMakeId { get; set; }
    public string MeterMakeName { get; set; } = string.Empty;
    public int? MeterModelId { get; set; }
    public string? MeterModelName { get; set; }
    public string Status { get; set; } = string.Empty;
    public int BaudRate { get; set; }
    public string ParityBit { get; set; } = string.Empty;
    public int StopBit { get; set; }
    public int DataBit { get; set; }

    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
}
