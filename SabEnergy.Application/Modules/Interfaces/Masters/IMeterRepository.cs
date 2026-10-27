using FluentResults;
using SabEnergy.Domain.Modules.Masters;

namespace SabEnergy.Application.Modules.Interfaces.Masters;

public interface IMeterRepository
{
    Task<Result<IEnumerable<Meter>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<Meter>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<Meter>> CreateAsync(Meter entity, CancellationToken cancellationToken = default);
    Task<Result<Meter>> UpdateAsync(Meter entity, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
