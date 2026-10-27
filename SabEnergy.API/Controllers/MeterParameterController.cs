using Microsoft.AspNetCore.Mvc;
using SabEnergy.Application.Common.Models;
using SabEnergy.Application.Modules.DTOs.Masters;
using SabEnergy.Application.Modules.Interfaces.Masters;

namespace SabEnergy.API.Controllers;

[Route("api/[controller]")]
public class MeterParameterController : BaseApiController
{
    private readonly IMeterParameterService _meterParameterService;

    public MeterParameterController(IMeterParameterService meterParameterService)
    {
        _meterParameterService = meterParameterService;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _meterParameterService.GetAllAsync(cancellationToken);
        return HandleResult(result, "Meter parameters retrieved successfully.");
    }

    [HttpGet("GetById")]
    public async Task<ActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _meterParameterService.GetByIdAsync(id, cancellationToken);
        return HandleResult(result, "Meter parameter retrieved successfully.");
    }

    [HttpPost("Create")]
    public async Task<ActionResult> Create([FromBody] CreateMeterParameterDto dto, CancellationToken cancellationToken)
    {
        var result = await _meterParameterService.CreateAsync(dto, cancellationToken);
        return HandleResult(result, "Meter parameter created successfully.");
    }

    [HttpPut("Update")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateMeterParameterDto dto, CancellationToken cancellationToken)
    {
        var result = await _meterParameterService.UpdateAsync(id, dto, cancellationToken);
        return HandleResult(result, "Meter parameter updated successfully.");
    }

    [HttpDelete("Delete")]
    public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _meterParameterService.DeleteAsync(id, cancellationToken);
        return HandleResult(result, "Meter parameter deleted successfully.");
    }
}
