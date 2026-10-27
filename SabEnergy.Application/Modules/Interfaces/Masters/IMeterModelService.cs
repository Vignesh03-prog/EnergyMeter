using FluentResults;
using SabEnergy.Application.Modules.DTOs.Masters;

namespace SabEnergy.Application.Modules.Interfaces.Masters;

public interface IMeterModelService
{
    Task<Result<IEnumerable<MeterModelDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<MeterModelDto>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<MeterModelDto>> CreateAsync(CreateMeterModelDto dto, CancellationToken cancellationToken = default);
    Task<Result<MeterModelDto>> UpdateAsync(int id, UpdateMeterModelDto dto, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
