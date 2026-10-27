using SabEnergy.Domain.Common;

namespace SabEnergy.Domain.Modules.Masters;

public sealed class MeterParameter : BaseEntity
{
    public int MeterProfileId { get; set; }
    public int ParameterMasterId { get; set; }
    public int Address { get; set; }
    public int DataType { get; set; }
    public int Multiplier { get; set; }

    public MeterProfile? MeterProfile { get; set; }
    public ParameterMaster? ParameterMaster { get; set; }
}