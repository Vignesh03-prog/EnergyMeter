using Microsoft.AspNetCore.Mvc;
using SabEnergy.Application.Common.Models;
using SabEnergy.Application.Modules.DTOs.Masters;
using SabEnergy.Application.Modules.Interfaces.Masters;

namespace SabEnergy.API.Controllers;

[Route("api/[controller]")]
public class MeterProfileController : BaseApiController
{
    private readonly IMeterProfileService _meterProfileService;

    public MeterProfileController(IMeterProfileService meterProfileService)
    {
        _meterProfileService = meterProfileService;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _meterProfileService.GetAllAsync(cancellationToken);
        return HandleResult(result, "Meter profiles retrieved successfully.");
    }

    [HttpGet("GetById")]
    public async Task<ActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _meterProfileService.GetByIdAsync(id, cancellationToken);
        return HandleResult(result, "Meter profile retrieved successfully.");
    }

    [HttpPost("Create")]
    public async Task<ActionResult> Create([FromBody] CreateMeterProfileDto dto, CancellationToken cancellationToken)
    {
        var result = await _meterProfileService.CreateAsync(dto, cancellationToken);
        return HandleResult(result, "Meter profile created successfully.");
    }

    [HttpPut("Update")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateMeterProfileDto dto, CancellationToken cancellationToken)
    {
        var result = await _meterProfileService.UpdateAsync(id, dto, cancellationToken);
        return HandleResult(result, "Meter profile updated successfully.");
    }

    [HttpDelete("Delete")]
    public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _meterProfileService.DeleteAsync(id, cancellationToken);
        return HandleResult(result, "Meter profile deleted successfully.");
    }
}
