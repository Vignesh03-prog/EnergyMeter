using FluentResults;
using SabEnergy.Application.Modules.DTOs.Masters;

namespace SabEnergy.Application.Modules.Interfaces.Masters;

public interface IGatewayModelService
{
    Task<Result<IEnumerable<GatewayModelDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<GatewayModelDto>> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<GatewayModelDto>> CreateAsync(CreateGatewayModelDto dto, CancellationToken cancellationToken = default);
    Task<Result<GatewayModelDto>> UpdateAsync(int id, UpdateGatewayModelDto dto, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
