namespace SabEnergy.Application.Modules.BO.Masters;

public class MeterModelBo
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
    public bool IsDeleted { get; set; }
}
