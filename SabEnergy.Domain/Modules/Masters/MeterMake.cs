using SabEnergy.Domain.Common;

namespace SabEnergy.Domain.Modules.Masters;

public sealed class MeterMake : BaseEntity
{
    public string MeterMakeName { get; set; } = string.Empty;
}