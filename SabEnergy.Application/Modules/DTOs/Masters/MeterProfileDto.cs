namespace SabEnergy.Application.Modules.DTOs.Masters;

public class MeterProfileDto
{
    public int Id { get; set; }
    public string ProfileVersion { get; set; } = string.Empty;
    public int MakeId { get; set; }
    public string MakeName { get; set; } = string.Empty;
    public int ModelId { get; set; }
    public string ModelName { get; set; } = string.Empty;
    public int AddressOffest { get; set; }
    public int HoldingRegister { get; set; }
    public int ByteOrder { get; set; }
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
}

public class CreateMeterProfileDto
{
    public string ProfileVersion { get; set; } = string.Empty;
    public int MakeId { get; set; }
    public int ModelId { get; set; }
    public int AddressOffest { get; set; }
    public int HoldingRegister { get; set; }
    public int ByteOrder { get; set; }
}

public class UpdateMeterProfileDto
{
    public string ProfileVersion { get; set; } = string.Empty;
    public int MakeId { get; set; }
    public int ModelId { get; set; }
    public int AddressOffest { get; set; }
    public int HoldingRegister { get; set; }
    public int ByteOrder { get; set; }
    public bool IsActive { get; set; } = true;
}
