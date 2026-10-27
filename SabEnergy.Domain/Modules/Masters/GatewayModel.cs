using SabEnergy.Domain.Common;

namespace SabEnergy.Domain.Modules.Masters.Model;

public sealed class GatewayModel : BaseEntity
{
    public string GatewayModelName { get; set; } = string.Empty;
    public int GatewayMakeId { get; set; }
    public GatewayMake? GatewayMake { get; set; }
}