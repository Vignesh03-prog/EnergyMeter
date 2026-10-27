namespace SabEnergy.Application.Modules.BO.Masters;

public class MeterMakeBo
{
    public int Id { get; set; }
    public string MeterMakeName { get; set; } = string.Empty;
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
}
