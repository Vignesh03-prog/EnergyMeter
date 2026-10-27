using Microsoft.AspNetCore.Mvc;
using SabEnergy.Application.Common.Models;
using SabEnergy.Application.Modules.DTOs.Masters;
using SabEnergy.Application.Modules.Interfaces.Masters;

namespace SabEnergy.API.Controllers;

[Route("api/[controller]")]
public class MakeController : BaseApiController
{
    private readonly IGatewayMakeService _gatewayMakeService;
    private readonly IMeterMakeService _meterMakeService;

    public MakeController(
        IGatewayMakeService gatewayMakeService,
        IMeterMakeService meterMakeService)
    {
        _gatewayMakeService = gatewayMakeService;
        _meterMakeService = meterMakeService;
    }

    [HttpGet("gateway-makes/GetAll")]
    public async Task<ActionResult> GetAllGatewayMakes(CancellationToken cancellationToken)
    {
        var result = await _gatewayMakeService.GetAllAsync(cancellationToken);
        return HandleResult(result, "Gateway makes retrieved successfully.");
    }

    [HttpGet("gateway-makes/GetById")]
    public async Task<ActionResult> GetGatewayMakeById(int id, CancellationToken cancellationToken)
    {
        var result = await _gatewayMakeService.GetByIdAsync(id, cancellationToken);
        return HandleResult(result, "Gateway make retrieved successfully.");
    }

    [HttpPost("gateway-makes/Create")]
    public async Task<ActionResult> CreateGatewayMake([FromBody] CreateGatewayMakeDto dto, CancellationToken cancellationToken)
    {
        var result = await _gatewayMakeService.CreateAsync(dto, cancellationToken);
        return HandleResult(result, "Gateway make created successfully.");
    }

    [HttpPut("gateway-makes/Update")]
    public async Task<ActionResult> UpdateGatewayMake(int id, [FromBody] UpdateGatewayMakeDto dto, CancellationToken cancellationToken)
    {
        var result = await _gatewayMakeService.UpdateAsync(id, dto, cancellationToken);
        return HandleResult(result, "Gateway make updated successfully.");
    }

    [HttpDelete("gateway-makes/Delete")]
    public async Task<ActionResult> DeleteGatewayMake(int id, CancellationToken cancellationToken)
    {
        var result = await _gatewayMakeService.DeleteAsync(id, cancellationToken);
        return HandleResult(result, "Gateway make deleted successfully.");
    }

    [HttpGet("meter-makes/GetAll")]
    public async Task<ActionResult> GetAllMeterMakes(CancellationToken cancellationToken)
    {
        var result = await _meterMakeService.GetAllAsync(cancellationToken);
        return HandleResult(result, "Meter makes retrieved successfully.");
    }

    [HttpGet("meter-makes/GetById")]
    public async Task<ActionResult> GetMeterMakeById(int id, CancellationToken cancellationToken)
    {
        var result = await _meterMakeService.GetByIdAsync(id, cancellationToken);
        return HandleResult(result, "Meter make retrieved successfully.");
    }

    [HttpPost("meter-makes/Create")]
    public async Task<ActionResult> CreateMeterMake([FromBody] CreateMeterMakeDto dto, CancellationToken cancellationToken)
    {
        var result = await _meterMakeService.CreateAsync(dto, cancellationToken);
        return HandleResult(result, "Meter make created successfully.");
    }

    [HttpPut("meter-makes/Update")]
    public async Task<ActionResult> UpdateMeterMake(int id, [FromBody] UpdateMeterMakeDto dto, CancellationToken cancellationToken)
    {
        var result = await _meterMakeService.UpdateAsync(id, dto, cancellationToken);
        return HandleResult(result, "Meter make updated successfully.");
    }

    [HttpDelete("meter-makes/Delete")]
    public async Task<ActionResult> DeleteMeterMake(int id, CancellationToken cancellationToken)
    {
        var result = await _meterMakeService.DeleteAsync(id, cancellationToken);
        return HandleResult(result, "Meter make deleted successfully.");
    }
}
