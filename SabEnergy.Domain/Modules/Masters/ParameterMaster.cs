using SabEnergy.Domain.Common;

namespace SabEnergy.Domain.Modules.Masters;

public sealed class ParameterMaster : BaseEntity
{
    public string Key { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public string Unit { get; set; } = string.Empty;
}
