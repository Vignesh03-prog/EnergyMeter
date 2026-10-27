using SabEnergy.Domain.Common;

namespace SabEnergy.Domain.Modules.Masters;

public sealed class GatewayMake : BaseEntity
{
    public string GatewayMakeName { get; set; } = string.Empty;
}