using FluentResults;
using SabEnergy.Application.Modules.BO.Masters;
using SabEnergy.Application.Modules.DTOs.Masters;
using SabEnergy.Application.Modules.Interfaces.Masters;
using SabEnergy.Domain.Modules.Masters;

namespace SabEnergy.Application.Modules.Services.Masters;

public class GatewayMakeService : IGatewayMakeService
{
    private readonly IGatewayMakeRepository _repository;

    public GatewayMakeService(IGatewayMakeRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<GatewayMakeDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAllAsync(cancellationToken);
        if (result.IsFailed)
            return Result.Fail<IEnumerable<GatewayMakeDto>>(result.Errors);

        var dtos = result.Value
            .Select(MapEntityToBo)
            .Select(MapBoToDto);

        return Result.Ok<IEnumerable<GatewayMakeDto>>(dtos);
    }

    public async Task<Result<GatewayMakeDto>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetByIdAsync(id, cancellationToken);
        if (result.IsFailed)
            return Result.Fail<GatewayMakeDto>(result.Errors);

        var bo = MapEntityToBo(result.Value);
        return Result.Ok(MapBoToDto(bo));
    }

    public async Task<Result<GatewayMakeDto>> CreateAsync(CreateGatewayMakeDto dto, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(dto.GatewayMakeName))
            return Result.Fail<GatewayMakeDto>("Gateway make name is required.");

        var bo = new GatewayMakeBo
        {
            GatewayMakeName = dto.GatewayMakeName.Trim(),
            CreatedBy = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        var entity = MapBoToEntity(bo);
        var result = await _repository.CreateAsync(entity, cancellationToken);
        if (result.IsFailed)
            return Result.Fail<GatewayMakeDto>(result.Errors);

        var savedBo = MapEntityToBo(result.Value);
        return Result.Ok(MapBoToDto(savedBo));
    }

    public async Task<Result<GatewayMakeDto>> UpdateAsync(int id, UpdateGatewayMakeDto dto, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(dto.GatewayMakeName))
            return Result.Fail<GatewayMakeDto>("Gateway make name is required.");

        var bo = new GatewayMakeBo
        {
            Id = id,
            GatewayMakeName = dto.GatewayMakeName.Trim(),
            IsActive = dto.IsActive,
            UpdatedBy = 1,
            UpdatedAt = DateTime.UtcNow
        };

        var entity = MapBoToEntity(bo);
        var result = await _repository.UpdateAsync(entity, cancellationToken);
        if (result.IsFailed)
            return Result.Fail<GatewayMakeDto>(result.Errors);

        var updatedBo = MapEntityToBo(result.Value);
        return Result.Ok(MapBoToDto(updatedBo));
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _repository.DeleteAsync(id, cancellationToken);
    }

    private static GatewayMakeBo MapEntityToBo(GatewayMake entity) => new()
    {
        Id = entity.Id,
        GatewayMakeName = entity.GatewayMakeName,
        CreatedBy = entity.CreatedBy,
        UpdatedBy = entity.UpdatedBy,
        CreatedAt = entity.CreatedAt,
        UpdatedAt = entity.UpdatedAt,
        IsActive = entity.IsActive,
        IsDeleted = entity.IsDeleted
    };

    private static GatewayMake MapBoToEntity(GatewayMakeBo bo) => new()
    {
        Id = bo.Id,
        GatewayMakeName = bo.GatewayMakeName,
        CreatedBy = bo.CreatedBy,
        UpdatedBy = bo.UpdatedBy,
        CreatedAt = bo.CreatedAt,
        UpdatedAt = bo.UpdatedAt,
        IsActive = bo.IsActive,
        IsDeleted = bo.IsDeleted
    };

    private static GatewayMakeDto MapBoToDto(GatewayMakeBo bo) => new()
    {
        Id = bo.Id,
        GatewayMakeName = bo.GatewayMakeName,
        CreatedBy = bo.CreatedBy,
        UpdatedBy = bo.UpdatedBy,
        CreatedAt = bo.CreatedAt,
        UpdatedAt = bo.UpdatedAt,
        IsActive = bo.IsActive
    };
}
