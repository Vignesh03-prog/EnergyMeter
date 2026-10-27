using FluentResults;
using SabEnergy.Domain.Modules.Masters;

namespace SabEnergy.Application.Modules.Interfaces.Masters;

public interface IMeterProfileRepository
{
    Task<Result<IEnumerable<MeterProfile>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<MeterProfile>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<MeterProfile>> CreateAsync(MeterProfile entity, CancellationToken cancellationToken = default);
    Task<Result<MeterProfile>> UpdateAsync(MeterProfile entity, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
