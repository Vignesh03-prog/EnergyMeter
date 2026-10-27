using FluentResults;
using SabEnergy.Application.Modules.DTOs.Masters;

namespace SabEnergy.Application.Modules.Interfaces.Masters;

public interface IMeterParameterService
{
    Task<Result<IEnumerable<MeterParameterDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<MeterParameterDto>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<MeterParameterDto>> CreateAsync(CreateMeterParameterDto dto, CancellationToken cancellationToken = default);
    Task<Result<MeterParameterDto>> UpdateAsync(int id, UpdateMeterParameterDto dto, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
