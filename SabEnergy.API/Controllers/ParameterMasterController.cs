using Microsoft.AspNetCore.Mvc;
using SabEnergy.Application.Common.Models;
using SabEnergy.Application.Modules.DTOs.Masters;
using SabEnergy.Application.Modules.Interfaces.Masters;

namespace SabEnergy.API.Controllers;

[Route("api/[controller]")]
public class ParameterMasterController : BaseApiController
{
    private readonly IParameterMasterService _parameterMasterService;

    public ParameterMasterController(IParameterMasterService parameterMasterService)
    {
        _parameterMasterService = parameterMasterService;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _parameterMasterService.GetAllAsync(cancellationToken);
        return HandleResult(result, "Parameter masters retrieved successfully.");
    }

    [HttpGet("GetById")]
    public async Task<ActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _parameterMasterService.GetByIdAsync(id, cancellationToken);
        return HandleResult(result, "Parameter master retrieved successfully.");
    }

    [HttpPost("Create")]
    public async Task<ActionResult> Create([FromBody] CreateParameterMasterDto dto, CancellationToken cancellationToken)
    {
        var result = await _parameterMasterService.CreateAsync(dto, cancellationToken);
        return HandleResult(result, "Parameter master created successfully.");
    }

    [HttpPut("Update")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateParameterMasterDto dto, CancellationToken cancellationToken)
    {
        var result = await _parameterMasterService.UpdateAsync(id, dto, cancellationToken);
        return HandleResult(result, "Parameter master updated successfully.");
    }

    [HttpDelete("Delete")]
    public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _parameterMasterService.DeleteAsync(id, cancellationToken);
        return HandleResult(result, "Parameter master deleted successfully.");
    }
}
