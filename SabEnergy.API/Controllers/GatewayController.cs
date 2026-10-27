using Microsoft.AspNetCore.Mvc;
using SabEnergy.Application.Common.Models;
using SabEnergy.Application.Modules.DTOs.Masters;
using SabEnergy.Application.Modules.Interfaces.Masters;

namespace SabEnergy.API.Controllers;

[Route("api/[controller]")]
public class GatewayController : BaseApiController
{
    private readonly IGatewayService _gatewayService;

    public GatewayController(IGatewayService gatewayService)
    {
        _gatewayService = gatewayService;
    }

    [HttpGet("gateways/GetAll")]
    public async Task<ActionResult> GetAllGateways(CancellationToken cancellationToken)
    {
        var result = await _gatewayService.GetAllAsync(cancellationToken);
        return HandleResult(result, "Gateways retrieved successfully.");
    }

    [HttpGet("gateways/GetById")]
    public async Task<ActionResult> GetGatewayById(int id, CancellationToken cancellationToken)
    {
        var result = await _gatewayService.GetByIdAsync(id, cancellationToken);
        return HandleResult(result, "Gateway retrieved successfully.");
    }

    [HttpPost("gateways/Create")]
    public async Task<ActionResult> CreateGateway([FromBody] CreateGatewayDto dto, CancellationToken cancellationToken)
    {
        var result = await _gatewayService.CreateAsync(dto, cancellationToken);
        return HandleResult(result, "Gateway created successfully.");
    }

    [HttpPut("gateways/Update")]
    public async Task<ActionResult> UpdateGateway(int id, [FromBody] UpdateGatewayDto dto, CancellationToken cancellationToken)
    {
        var result = await _gatewayService.UpdateAsync(id, dto, cancellationToken);
        return HandleResult(result, "Gateway updated successfully.");
    }

    [HttpDelete("gateways/Delete")]
    public async Task<ActionResult> DeleteGateway(int id, CancellationToken cancellationToken)
    {
        var result = await _gatewayService.DeleteAsync(id, cancellationToken);
        return HandleResult(result, "Gateway deleted successfully.");
    }
}
