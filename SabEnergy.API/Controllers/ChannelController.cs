using Microsoft.AspNetCore.Mvc;
using SabEnergy.Application.Common.Models;
using SabEnergy.Application.Modules.DTOs.Masters;
using SabEnergy.Application.Modules.Interfaces.Masters;

namespace SabEnergy.API.Controllers;

[Route("api/[controller]")]
public class ChannelController : BaseApiController
{
    private readonly IChannelService _channelService;

    public ChannelController(IChannelService channelService)
    {
        _channelService = channelService;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _channelService.GetAllAsync(cancellationToken);
        return HandleResult(result, "Channels retrieved successfully.");
    }

    [HttpGet("GetById")]
    public async Task<ActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var result = await _channelService.GetByIdAsync(id, cancellationToken);
        return HandleResult(result, "Channel retrieved successfully.");
    }

    [HttpGet("GetByGatewayId")]
    public async Task<ActionResult> GetByGatewayId(int gatewayId, CancellationToken cancellationToken)
    {
        var result = await _channelService.GetByGatewayIdAsync(gatewayId, cancellationToken);
        return HandleResult(result, "Channels for gateway retrieved successfully.");
    }

    [HttpPost("Create")]
    public async Task<ActionResult> Create([FromBody] CreateChannelDto dto, CancellationToken cancellationToken)
    {
        var result = await _channelService.CreateAsync(dto, cancellationToken);
        return HandleResult(result, "Channel created successfully.");
    }

    [HttpPut("Update")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateChannelDto dto, CancellationToken cancellationToken)
    {
        var result = await _channelService.UpdateAsync(id, dto, cancellationToken);
        return HandleResult(result, "Channel updated successfully.");
    }

    [HttpDelete("Delete")]
    public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _channelService.DeleteAsync(id, cancellationToken);
        return HandleResult(result, "Channel deleted successfully.");
    }
}

