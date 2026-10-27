using SabEnergy.Domain.Common;

namespace SabEnergy.Domain.Modules.Masters.Model;

public sealed class MeterModel : BaseEntity
{
    public string MeterModelName { get; set; } = string.Empty;
    public int MeterMakeId { get; set; }
    public MeterMake? MeterMake { get; set; }
}
