using SabEnergy.Domain.Common;
using SabEnergy.Domain.Modules.Masters.Model;

namespace SabEnergy.Domain.Modules.Masters;

public sealed class MeterProfile : BaseEntity
{
    public string ProfileVersion { get; set; } = string.Empty;
    public int MakeId { get; set; }
    public int ModelId { get; set; }
    public int AddressOffest { get; set; }
    public int HoldingRegister { get; set; }
    public int ByteOrder { get; set; }

    public MeterMake? MeterMake { get; set; }
    public MeterModel? MeterModel { get; set; }
}