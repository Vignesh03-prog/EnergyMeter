using FluentResults;
using SabEnergy.Domain.Modules.Masters;

namespace SabEnergy.Application.Modules.Interfaces.Masters;

public interface IParameterMasterRepository
{
    Task<Result<IEnumerable<ParameterMaster>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<ParameterMaster>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<ParameterMaster>> CreateAsync(ParameterMaster entity, CancellationToken cancellationToken = default);
    Task<Result<ParameterMaster>> UpdateAsync(ParameterMaster entity, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
