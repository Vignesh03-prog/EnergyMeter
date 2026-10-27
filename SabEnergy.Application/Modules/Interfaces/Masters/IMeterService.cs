using FluentResults;
using SabEnergy.Application.Modules.DTOs.Masters;

namespace SabEnergy.Application.Modules.Interfaces.Masters;

public interface IMeterService
{
    Task<Result<IEnumerable<MeterDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<MeterDto>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<MeterDto>> CreateAsync(CreateMeterDto dto, CancellationToken cancellationToken = default);
    Task<Result<MeterDto>> UpdateAsync(int id, UpdateMeterDto dto, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
