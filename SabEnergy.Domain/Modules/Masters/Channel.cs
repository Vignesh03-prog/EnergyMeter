using SabEnergy.Domain.Common;

namespace SabEnergy.Domain.Modules.Masters;

public sealed class Channel : BaseEntity
{
    public string ChannelName { get; set; } = string.Empty;
    public int GatewayId { get; set; }
    public Gateway? Gateway { get; set; }
}
