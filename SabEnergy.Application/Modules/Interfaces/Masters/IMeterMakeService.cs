using FluentResults;
using SabEnergy.Application.Modules.DTOs.Masters;

namespace SabEnergy.Application.Modules.Interfaces.Masters;

public interface IMeterMakeService
{
    Task<Result<IEnumerable<MeterMakeDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<MeterMakeDto>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<MeterMakeDto>> CreateAsync(CreateMeterMakeDto dto, CancellationToken cancellationToken = default);
    Task<Result<MeterMakeDto>> UpdateAsync(int id, UpdateMeterMakeDto dto, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
