using FluentResults;
using SabEnergy.Domain.Modules.Masters;

namespace SabEnergy.Application.Modules.Interfaces.Masters;

public interface IGatewayMakeRepository
{
    Task<Result<IEnumerable<GatewayMake>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<GatewayMake>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<GatewayMake>> CreateAsync(GatewayMake entity, CancellationToken cancellationToken = default);
    Task<Result<GatewayMake>> UpdateAsync(GatewayMake entity, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
