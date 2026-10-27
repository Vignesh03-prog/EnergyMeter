namespace SabEnergy.Application.Modules.DTOs.Masters;

public class GatewayMakeDto
{
    public int Id { get; set; }
    public string GatewayMakeName { get; set; } = string.Empty;
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
}

public class CreateGatewayMakeDto
{
    public string GatewayMakeName { get; set; } = string.Empty;
}

public class UpdateGatewayMakeDto
{
    public string GatewayMakeName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}
