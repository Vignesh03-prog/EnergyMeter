using FluentResults;
using SabEnergy.Application.Modules.DTOs.Masters;

namespace SabEnergy.Application.Modules.Interfaces.Masters;

public interface IGatewayService
{
    Task<Result<IEnumerable<GatewayDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<GatewayDto>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<GatewayDto>> CreateAsync(CreateGatewayDto dto, CancellationToken cancellationToken = default);
    Task<Result<GatewayDto>> UpdateAsync(int id, UpdateGatewayDto dto, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
