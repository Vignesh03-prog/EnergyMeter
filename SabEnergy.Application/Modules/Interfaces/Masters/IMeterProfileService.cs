using FluentResults;
using SabEnergy.Application.Modules.DTOs.Masters;

namespace SabEnergy.Application.Modules.Interfaces.Masters;

public interface IMeterProfileService
{
    Task<Result<IEnumerable<MeterProfileDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<MeterProfileDto>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<MeterProfileDto>> CreateAsync(CreateMeterProfileDto dto, CancellationToken cancellationToken = default);
    Task<Result<MeterProfileDto>> UpdateAsync(int id, UpdateMeterProfileDto dto, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
