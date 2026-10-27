namespace SabEnergy.Application.Modules.DTOs.Masters;

public class MeterModelDto
{
    public int Id { get; set; }
    public string MeterModelName { get; set; } = string.Empty;
    public int MeterMakeId { get; set; }
    public string MeterMakeName { get; set; } = string.Empty;
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
}

public class CreateMeterModelDto
{
    public string MeterModelName { get; set; } = string.Empty;
    public int MeterMakeId { get; set; }
}

public class UpdateMeterModelDto
{
    public string MeterModelName { get; set; } = string.Empty;
    public int MeterMakeId { get; set; }
    public bool IsActive { get; set; } = true;
}
