using SabEnergy.Domain.Common;
using SabEnergy.Domain.Modules.Masters.Model;

namespace SabEnergy.Domain.Modules.Masters;

public sealed class Meter : BaseEntity
{
    public string SerialNo { get; set; } = string.Empty;
    public string CustomName { get; set; } = string.Empty;
    public int ChannelId { get; set; }
    public int SlaveId { get; set; }
    public int DeviceProfileId { get; set; }
    public int MeterMakeId { get; set; }
    public int? MeterModelId { get; set; }
    public string Status { get; set; } = string.Empty;
    public int BaudRate { get; set; }
    public string ParityBit { get; set; } = string.Empty;
    public int StopBit { get; set; }
    public int DataBit { get; set; }

    public MeterMake? MeterMake { get; set; }
    public MeterModel? MeterModel { get; set; }
}