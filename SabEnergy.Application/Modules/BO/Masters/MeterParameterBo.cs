namespace SabEnergy.Application.Modules.BO.Masters;

public class MeterParameterBo
{
    public int Id { get; set; }
    public int MeterProfileId { get; set; }
    public string ProfileVersion { get; set; } = string.Empty;
    public int ParameterMasterId { get; set; }
    public string Key { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public string Unit { get; set; } = string.Empty;
    public int Address { get; set; }
    public int DataType { get; set; }
    public int Multiplier { get; set; }
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
}
