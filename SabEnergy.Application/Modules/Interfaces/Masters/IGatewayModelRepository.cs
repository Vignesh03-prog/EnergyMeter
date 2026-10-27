using FluentResults;
using SabEnergy.Domain.Modules.Masters.Model;

namespace SabEnergy.Application.Modules.Interfaces.Masters;

public interface IGatewayModelRepository
{
    Task<Result<IEnumerable<GatewayModel>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<GatewayModel>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<GatewayModel>> CreateAsync(GatewayModel entity, CancellationToken cancellationToken = default);
    Task<Result<GatewayModel>> UpdateAsync(GatewayModel entity, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
