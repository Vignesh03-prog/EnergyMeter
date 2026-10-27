namespace SabEnergy.Application.Modules.DTOs.Masters;

public class MeterMakeDto
{
    public int Id { get; set; }
    public string MeterMakeName { get; set; } = string.Empty;
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
}

public class CreateMeterMakeDto
{
    public string MeterMakeName { get; set; } = string.Empty;
}

public class UpdateMeterMakeDto
{
    public string MeterMakeName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}
