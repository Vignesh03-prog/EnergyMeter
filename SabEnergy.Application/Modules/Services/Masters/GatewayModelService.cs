using FluentResults;
using SabEnergy.Application.Modules.BO.Masters;
using SabEnergy.Application.Modules.DTOs.Masters;
using SabEnergy.Application.Modules.Interfaces.Masters;
using SabEnergy.Domain.Modules.Masters.Model;

namespace SabEnergy.Application.Modules.Services.Masters;

public class GatewayModelService : IGatewayModelService
{
    private readonly IGatewayModelRepository _repository;

    public GatewayModelService(IGatewayModelRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<GatewayModelDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAllAsync(cancellationToken);
        if (result.IsFailed)
            return Result.Fail<IEnumerable<GatewayModelDto>>(result.Errors);

        // Map Entity -> BO -> DTO
        var dtos = result.Value
            .Select(MapEntityToBo)
            .Select(MapBoToDto);

        return Result.Ok<IEnumerable<GatewayModelDto>>(dtos);
    }

    public async Task<Result<GatewayModelDto>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetByIdAsync(id, cancellationToken);
        if (result.IsFailed)
            return Result.Fail<GatewayModelDto>(result.Errors);

        var bo = MapEntityToBo(result.Value);
        return Result.Ok(MapBoToDto(bo));
    }

    public async Task<Result<GatewayModelDto>> CreateAsync(CreateGatewayModelDto dto, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(dto.GatewayModelName))
            return Result.Fail<GatewayModelDto>("Gateway model name is required.");

        if (dto.GatewayMakeId <= 0)
            return Result.Fail<GatewayModelDto>("Valid Gateway Make ID is required.");

        // Map DTO -> BO -> Entity
        var bo = new GatewayModelBo
        {
            GatewayModelName = dto.GatewayModelName.Trim(),
            GatewayMakeId = dto.GatewayMakeId,
            CreatedBy = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        var entity = MapBoToEntity(bo);
        var result = await _repository.CreateAsync(entity, cancellationToken);
        if (result.IsFailed)
            return Result.Fail<GatewayModelDto>(result.Errors);

        var savedBo = MapEntityToBo(result.Value);
        return Result.Ok(MapBoToDto(savedBo));
    }

    public async Task<Result<GatewayModelDto>> UpdateAsync(int id, UpdateGatewayModelDto dto, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(dto.GatewayModelName))
            return Result.Fail<GatewayModelDto>("Gateway model name is required.");

        if (dto.GatewayMakeId <= 0)
            return Result.Fail<GatewayModelDto>("Valid Gateway Make ID is required.");

        // Map DTO -> BO -> Entity
        var bo = new GatewayModelBo
        {
            Id = id,
            GatewayModelName = dto.GatewayModelName.Trim(),
            GatewayMakeId = dto.GatewayMakeId,
            IsActive = dto.IsActive,
            UpdatedBy = 1,
            UpdatedAt = DateTime.UtcNow
        };

        var entity = MapBoToEntity(bo);
        var result = await _repository.UpdateAsync(entity, cancellationToken);
        if (result.IsFailed)
            return Result.Fail<GatewayModelDto>(result.Errors);

        var updatedBo = MapEntityToBo(result.Value);
        return Result.Ok(MapBoToDto(updatedBo));
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _repository.DeleteAsync(id, cancellationToken);
    }

    // Mapping Entity <-> BO
    private static GatewayModelBo MapEntityToBo(GatewayModel entity) => new()
    {
        Id = entity.Id,
        GatewayModelName = entity.GatewayModelName,
        GatewayMakeId = entity.GatewayMakeId,
        GatewayMakeName = entity.GatewayMake?.GatewayMakeName ?? string.Empty,
        CreatedBy = entity.CreatedBy,
        UpdatedBy = entity.UpdatedBy,
        CreatedAt = entity.CreatedAt,
        UpdatedAt = entity.UpdatedAt,
        IsActive = entity.IsActive,
        IsDeleted = entity.IsDeleted
    };

    private static GatewayModel MapBoToEntity(GatewayModelBo bo) => new()
    {
        Id = bo.Id,
        GatewayModelName = bo.GatewayModelName,
        GatewayMakeId = bo.GatewayMakeId,
        CreatedBy = bo.CreatedBy,
        UpdatedBy = bo.UpdatedBy,
        CreatedAt = bo.CreatedAt,
        UpdatedAt = bo.UpdatedAt,
        IsActive = bo.IsActive,
        IsDeleted = bo.IsDeleted
    };

    // Mapping BO <-> DTO
    private static GatewayModelDto MapBoToDto(GatewayModelBo bo) => new()
    {
        Id = bo.Id,
        GatewayModelName = bo.GatewayModelName,
        GatewayMakeId = bo.GatewayMakeId,
        GatewayMakeName = bo.GatewayMakeName,
        CreatedBy = bo.CreatedBy,
        UpdatedBy = bo.UpdatedBy,
        CreatedAt = bo.CreatedAt,
        UpdatedAt = bo.UpdatedAt,
        IsActive = bo.IsActive
    };
}
