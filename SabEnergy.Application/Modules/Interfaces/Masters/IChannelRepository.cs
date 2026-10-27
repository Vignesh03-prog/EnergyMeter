using FluentResults;
using SabEnergy.Domain.Modules.Masters;

namespace SabEnergy.Application.Modules.Interfaces.Masters;

public interface IChannelRepository
{
    Task<Result<IEnumerable<Channel>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<Channel>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<Channel>>> GetByGatewayIdAsync(int gatewayId, CancellationToken cancellationToken = default);
    Task<Result<Channel>> CreateAsync(Channel entity, CancellationToken cancellationToken = default);
    Task<Result<Channel>> UpdateAsync(Channel entity, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
}

