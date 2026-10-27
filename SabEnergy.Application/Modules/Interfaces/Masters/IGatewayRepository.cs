using FluentResults;
using SabEnergy.Domain.Modules.Masters;

namespace SabEnergy.Application.Modules.Interfaces.Masters;

public interface IGatewayRepository
{
    Task<Result<IEnumerable<Gateway>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<Gateway>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<Gateway>> CreateAsync(Gateway entity, CancellationToken cancellationToken = default);
    Task<Result<Gateway>> UpdateAsync(Gateway entity, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
