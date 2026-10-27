using FluentResults;
using SabEnergy.Domain.Modules.Masters;

namespace SabEnergy.Application.Modules.Interfaces.Masters;

public interface IMeterMakeRepository
{
    Task<Result<IEnumerable<MeterMake>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<MeterMake>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<MeterMake>> CreateAsync(MeterMake entity, CancellationToken cancellationToken = default);
    Task<Result<MeterMake>> UpdateAsync(MeterMake entity, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
