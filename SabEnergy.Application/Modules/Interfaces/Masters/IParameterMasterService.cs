using FluentResults;
using SabEnergy.Application.Modules.DTOs.Masters;

namespace SabEnergy.Application.Modules.Interfaces.Masters;

public interface IParameterMasterService
{
    Task<Result<IEnumerable<ParameterMasterDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<ParameterMasterDto>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<ParameterMasterDto>> CreateAsync(CreateParameterMasterDto dto, CancellationToken cancellationToken = default);
    Task<Result<ParameterMasterDto>> UpdateAsync(int id, UpdateParameterMasterDto dto, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
