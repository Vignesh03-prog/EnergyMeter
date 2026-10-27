using FluentResults;
using SabEnergy.Domain.Modules.Masters;

namespace SabEnergy.Application.Modules.Interfaces.Masters;

public interface IMeterParameterRepository
{
    Task<Result<IEnumerable<MeterParameter>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<MeterParameter>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<MeterParameter>> CreateAsync(MeterParameter entity, CancellationToken cancellationToken = default);
    Task<Result<MeterParameter>> UpdateAsync(MeterParameter entity, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
