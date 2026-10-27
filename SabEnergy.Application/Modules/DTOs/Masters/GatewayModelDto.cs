namespace SabEnergy.Application.Modules.DTOs.Masters;

public class GatewayModelDto
{
    public int Id { get; set; }
    public string GatewayModelName { get; set; } = string.Empty;
    public int GatewayMakeId { get; set; }
    public string GatewayMakeName { get; set; } = string.Empty;
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
}

public class CreateGatewayModelDto
{
    public string GatewayModelName { get; set; } = string.Empty;
    public int GatewayMakeId { get; set; }
}

public class UpdateGatewayModelDto
{
    public string GatewayModelName { get; set; } = string.Empty;
    public int GatewayMakeId { get; set; }
    public bool IsActive { get; set; } = true;
}
