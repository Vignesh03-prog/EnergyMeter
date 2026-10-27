using FluentResults;
using SabEnergy.Application.Modules.DTOs.Masters;

namespace SabEnergy.Application.Modules.Interfaces.Masters;

public interface IGatewayMakeService
{
    Task<Result<IEnumerable<GatewayMakeDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<GatewayMakeDto>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<GatewayMakeDto>> CreateAsync(CreateGatewayMakeDto dto, CancellationToken cancellationToken = default);
    Task<Result<GatewayMakeDto>> UpdateAsync(int id, UpdateGatewayMakeDto dto, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
