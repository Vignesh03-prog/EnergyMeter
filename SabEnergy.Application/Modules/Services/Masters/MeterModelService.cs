using FluentResults;
using SabEnergy.Application.Modules.BO.Masters;
using SabEnergy.Application.Modules.DTOs.Masters;
using SabEnergy.Application.Modules.Interfaces.Masters;
using SabEnergy.Domain.Modules.Masters.Model;

namespace SabEnergy.Application.Modules.Services.Masters;

public class MeterModelService : IMeterModelService
{
    private readonly IMeterModelRepository _repository;

    public MeterModelService(IMeterModelRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<MeterModelDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAllAsync(cancellationToken);
        if (result.IsFailed)
            return Result.Fail<IEnumerable<MeterModelDto>>(result.Errors);

        // Map Entity -> BO -> DTO
        var dtos = result.Value
            .Select(MapEntityToBo)
            .Select(MapBoToDto);

        return Result.Ok<IEnumerable<MeterModelDto>>(dtos);
    }

    public async Task<Result<MeterModelDto>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetByIdAsync(id, cancellationToken);
        if (result.IsFailed)
            return Result.Fail<MeterModelDto>(result.Errors);

        var bo = MapEntityToBo(result.Value);
        return Result.Ok(MapBoToDto(bo));
    }

    public async Task<Result<MeterModelDto>> CreateAsync(CreateMeterModelDto dto, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(dto.MeterModelName))
            return Result.Fail<MeterModelDto>("Meter model name is required.");

        if (dto.MeterMakeId <= 0)
            return Result.Fail<MeterModelDto>("Valid Meter Make ID is required.");

        // Map DTO -> BO -> Entity
        var bo = new MeterModelBo
        {
            MeterModelName = dto.MeterModelName.Trim(),
            MeterMakeId = dto.MeterMakeId,
            CreatedBy = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        var entity = MapBoToEntity(bo);
        var result = await _repository.CreateAsync(entity, cancellationToken);
        if (result.IsFailed)
            return Result.Fail<MeterModelDto>(result.Errors);

        var savedBo = MapEntityToBo(result.Value);
        return Result.Ok(MapBoToDto(savedBo));
    }

    public async Task<Result<MeterModelDto>> UpdateAsync(int id, UpdateMeterModelDto dto, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(dto.MeterModelName))
            return Result.Fail<MeterModelDto>("Meter model name is required.");

        if (dto.MeterMakeId <= 0)
            return Result.Fail<MeterModelDto>("Valid Meter Make ID is required.");

        // Map DTO -> BO -> Entity
        var bo = new MeterModelBo
        {
            Id = id,
            MeterModelName = dto.MeterModelName.Trim(),
            MeterMakeId = dto.MeterMakeId,
            IsActive = dto.IsActive,
            UpdatedBy = 1,
            UpdatedAt = DateTime.UtcNow
        };

        var entity = MapBoToEntity(bo);
        var result = await _repository.UpdateAsync(entity, cancellationToken);
        if (result.IsFailed)
            return Result.Fail<MeterModelDto>(result.Errors);

        var updatedBo = MapEntityToBo(result.Value);
        return Result.Ok(MapBoToDto(updatedBo));
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _repository.DeleteAsync(id, cancellationToken);
    }

    // Mapping Entity <-> BO
    private static MeterModelBo MapEntityToBo(MeterModel entity) => new()
    {
        Id = entity.Id,
        MeterModelName = entity.MeterModelName,
        MeterMakeId = entity.MeterMakeId,
        MeterMakeName = entity.MeterMake?.MeterMakeName ?? string.Empty,
        CreatedBy = entity.CreatedBy,
        UpdatedBy = entity.UpdatedBy,
        CreatedAt = entity.CreatedAt,
        UpdatedAt = entity.UpdatedAt,
        IsActive = entity.IsActive,
        IsDeleted = entity.IsDeleted
    };

    private static MeterModel MapBoToEntity(MeterModelBo bo) => new()
    {
        Id = bo.Id,
        MeterModelName = bo.MeterModelName,
        MeterMakeId = bo.MeterMakeId,
        CreatedBy = bo.CreatedBy,
        UpdatedBy = bo.UpdatedBy,
        CreatedAt = bo.CreatedAt,
        UpdatedAt = bo.UpdatedAt,
        IsActive = bo.IsActive,
        IsDeleted = bo.IsDeleted
    };

    // Mapping BO <-> DTO
    private static MeterModelDto MapBoToDto(MeterModelBo bo) => new()
    {
        Id = bo.Id,
        MeterModelName = bo.MeterModelName,
        MeterMakeId = bo.MeterMakeId,
        MeterMakeName = bo.MeterMakeName,
        CreatedBy = bo.CreatedBy,
        UpdatedBy = bo.UpdatedBy,
        CreatedAt = bo.CreatedAt,
        UpdatedAt = bo.UpdatedAt,
        IsActive = bo.IsActive
    };
}
