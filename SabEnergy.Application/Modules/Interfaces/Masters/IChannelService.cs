using FluentResults;
using SabEnergy.Application.Modules.DTOs.Masters;

namespace SabEnergy.Application.Modules.Interfaces.Masters;

public interface IChannelService
{
    Task<Result<IEnumerable<ChannelDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<ChannelDto>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<ChannelDto>>> GetByGatewayIdAsync(int gatewayId, CancellationToken cancellationToken = default);
    Task<Result<ChannelDto>> CreateAsync(CreateChannelDto dto, CancellationToken cancellationToken = default);
    Task<Result<ChannelDto>> UpdateAsync(int id, UpdateChannelDto dto, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
}

