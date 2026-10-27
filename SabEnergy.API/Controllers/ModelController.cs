using Microsoft.AspNetCore.Mvc;
using SabEnergy.Application.Common.Models;
using SabEnergy.Application.Modules.DTOs.Masters;
using SabEnergy.Application.Modules.Interfaces.Masters;

namespace SabEnergy.API.Controllers;

[Route("api/[controller]")]
public class ModelController : BaseApiController
{
    private readonly IGatewayModelService _gatewayModelService;
    private readonly IMeterModelService _meterModelService;

    public ModelController(
        IGatewayModelService gatewayModelService,
        IMeterModelService meterModelService)
    {
        _gatewayModelService = gatewayModelService;
        _meterModelService = meterModelService;
    }

    [HttpGet("gateway-models/GetAll")]
    public async Task<ActionResult> GetAllGatewayModels(CancellationToken cancellationToken)
    {
        var result = await _gatewayModelService.GetAllAsync(cancellationToken);
        return HandleResult(result, "Gateway models retrieved successfully.");
    }

    [HttpGet("gateway-models/GetById")]
    public async Task<ActionResult> GetGatewayModelById(int id, CancellationToken cancellationToken)
    {
        var result = await _gatewayModelService.GetByIdAsync(id, cancellationToken);
        return HandleResult(result, "Gateway model retrieved successfully.");
    }

    [HttpPost("gateway-models/Create")]
    public async Task<ActionResult> CreateGatewayModel([FromBody] CreateGatewayModelDto dto, CancellationToken cancellationToken)
    {
        var result = await _gatewayModelService.CreateAsync(dto, cancellationToken);
        return HandleResult(result, "Gateway model created successfully.");
    }

    [HttpPut("gateway-models/Update")]
    public async Task<ActionResult> UpdateGatewayModel(int id, [FromBody] UpdateGatewayModelDto dto, CancellationToken cancellationToken)
    {
        var result = await _gatewayModelService.UpdateAsync(id, dto, cancellationToken);
        return HandleResult(result, "Gateway model updated successfully.");
    }

    [HttpDelete("gateway-models/Delete")]
    public async Task<ActionResult> DeleteGatewayModel(int id, CancellationToken cancellationToken)
    {
        var result = await _gatewayModelService.DeleteAsync(id, cancellationToken);
        return HandleResult(result, "Gateway model deleted successfully.");
    }

    [HttpGet("meter-models/GetAll")]
    public async Task<ActionResult> GetAllMeterModels(CancellationToken cancellationToken)
    {
        var result = await _meterModelService.GetAllAsync(cancellationToken);
        return HandleResult(result, "Meter models retrieved successfully.");
    }

    [HttpGet("meter-models/GetById")]
    public async Task<ActionResult> GetMeterModelById(int id, CancellationToken cancellationToken)
    {
        var result = await _meterModelService.GetByIdAsync(id, cancellationToken);
        return HandleResult(result, "Meter model retrieved successfully.");
    }

    [HttpPost("meter-models/Create")]
    public async Task<ActionResult> CreateMeterModel([FromBody] CreateMeterModelDto dto, CancellationToken cancellationToken)
    {
        var result = await _meterModelService.CreateAsync(dto, cancellationToken);
        return HandleResult(result, "Meter model created successfully.");
    }

    [HttpPut("meter-models/Update")]
    public async Task<ActionResult> UpdateMeterModel(int id, [FromBody] UpdateMeterModelDto dto, CancellationToken cancellationToken)
    {
        var result = await _meterModelService.UpdateAsync(id, dto, cancellationToken);
        return HandleResult(result, "Meter model updated successfully.");
    }

    [HttpDelete("meter-models/Delete")]
    public async Task<ActionResult> DeleteMeterModel(int id, CancellationToken cancellationToken)
    {
        var result = await _meterModelService.DeleteAsync(id, cancellationToken);
        return HandleResult(result, "Meter model deleted successfully.");
    }
}
