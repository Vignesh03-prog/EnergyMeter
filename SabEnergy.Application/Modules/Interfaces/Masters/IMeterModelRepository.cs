using FluentResults;
using SabEnergy.Domain.Modules.Masters.Model;

namespace SabEnergy.Application.Modules.Interfaces.Masters;

public interface IMeterModelRepository
{
    Task<Result<IEnumerable<MeterModel>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<MeterModel>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<MeterModel>> CreateAsync(MeterModel entity, CancellationToken cancellationToken = default);
    Task<Result<MeterModel>> UpdateAsync(MeterModel entity, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
