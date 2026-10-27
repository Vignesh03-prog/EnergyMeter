using Microsoft.AspNetCore.Mvc;
using SabEnergy.Application.Common.Models;
using SabEnergy.Application.Modules.DTOs.Masters;
using SabEnergy.Application.Modules.Interfaces.Masters;

namespace SabEnergy.API.Controllers;

[Route("api/[controller]")]
public class MeterController : BaseApiController
{
    private readonly IMeterService _meterService;

    public MeterController(IMeterService meterService)
    {
        _meterService = meterService;
    }

    [HttpGet("meters/GetAll")]
    public async Task<ActionResult> GetAllMeters(CancellationToken cancellationToken)
    {
        var result = await _meterService.GetAllAsync(cancellationToken);
        return HandleResult(result, "Meters retrieved successfully.");
    }

    [HttpGet("meters/GetById")]
    public async Task<ActionResult> GetMeterById(int id, CancellationToken cancellationToken)
    {
        var result = await _meterService.GetByIdAsync(id, cancellationToken);
        return HandleResult(result, "Meter retrieved successfully.");
    }

    [HttpPost("meters/Create")]
    public async Task<ActionResult> CreateMeter([FromBody] CreateMeterDto dto, CancellationToken cancellationToken)
    {
        var result = await _meterService.CreateAsync(dto, cancellationToken);
        return HandleResult(result, "Meter created successfully.");
    }

    [HttpPut("meters/Update")]
    public async Task<ActionResult> UpdateMeter(int id, [FromBody] UpdateMeterDto dto, CancellationToken cancellationToken)
    {
        var result = await _meterService.UpdateAsync(id, dto, cancellationToken);
        return HandleResult(result, "Meter updated successfully.");
    }

    [HttpDelete("meters/Delete")]
    public async Task<ActionResult> DeleteMeter(int id, CancellationToken cancellationToken)
    {
        var result = await _meterService.DeleteAsync(id, cancellationToken);
        return HandleResult(result, "Meter deleted successfully.");
    }
}
