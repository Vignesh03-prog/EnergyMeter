using FluentResults;
using SabEnergy.Application.Modules.BO.Masters;
using SabEnergy.Application.Modules.DTOs.Masters;
using SabEnergy.Application.Modules.Interfaces.Masters;
using SabEnergy.Domain.Modules.Masters;

namespace SabEnergy.Application.Modules.Services.Masters;

public class MeterMakeService : IMeterMakeService
{
    private readonly IMeterMakeRepository _repository;

    public MeterMakeService(IMeterMakeRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<MeterMakeDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAllAsync(cancellationToken);
        if (result.IsFailed)
            return Result.Fail<IEnumerable<MeterMakeDto>>(result.Errors);

        var dtos = result.Value
            .Select(MapEntityToBo)
            .Select(MapBoToDto);

        return Result.Ok<IEnumerable<MeterMakeDto>>(dtos);
    }

    public async Task<Result<MeterMakeDto>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetByIdAsync(id, cancellationToken);
        if (result.IsFailed)
            return Result.Fail<MeterMakeDto>(result.Errors);

        var bo = MapEntityToBo(result.Value);
        return Result.Ok(MapBoToDto(bo));
    }

    public async Task<Result<MeterMakeDto>> CreateAsync(CreateMeterMakeDto dto, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(dto.MeterMakeName))
            return Result.Fail<MeterMakeDto>("Meter make name is required.");

        var bo = new MeterMakeBo
        {
            MeterMakeName = dto.MeterMakeName.Trim(),
            CreatedBy = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        var entity = MapBoToEntity(bo);
        var result = await _repository.CreateAsync(entity, cancellationToken);
        if (result.IsFailed)
            return Result.Fail<MeterMakeDto>(result.Errors);

        var savedBo = MapEntityToBo(result.Value);
        return Result.Ok(MapBoToDto(savedBo));
    }

    public async Task<Result<MeterMakeDto>> UpdateAsync(int id, UpdateMeterMakeDto dto, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(dto.MeterMakeName))
            return Result.Fail<MeterMakeDto>("Meter make name is required.");

        var bo = new MeterMakeBo
        {
            Id = id,
            MeterMakeName = dto.MeterMakeName.Trim(),
            IsActive = dto.IsActive,
            UpdatedBy = 1,
            UpdatedAt = DateTime.UtcNow
        };

        var entity = MapBoToEntity(bo);
        var result = await _repository.UpdateAsync(entity, cancellationToken);
        if (result.IsFailed)
            return Result.Fail<MeterMakeDto>(result.Errors);

        var updatedBo = MapEntityToBo(result.Value);
        return Result.Ok(MapBoToDto(updatedBo));
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _repository.DeleteAsync(id, cancellationToken);
    }

    private static MeterMakeBo MapEntityToBo(MeterMake entity) => new()
    {
        Id = entity.Id,
        MeterMakeName = entity.MeterMakeName,
        CreatedBy = entity.CreatedBy,
        UpdatedBy = entity.UpdatedBy,
        CreatedAt = entity.CreatedAt,
        UpdatedAt = entity.UpdatedAt,
        IsActive = entity.IsActive,
        IsDeleted = entity.IsDeleted
    };

    private static MeterMake MapBoToEntity(MeterMakeBo bo) => new()
    {
        Id = bo.Id,
        MeterMakeName = bo.MeterMakeName,
        CreatedBy = bo.CreatedBy,
        UpdatedBy = bo.UpdatedBy,
        CreatedAt = bo.CreatedAt,
        UpdatedAt = bo.UpdatedAt,
        IsActive = bo.IsActive,
        IsDeleted = bo.IsDeleted
    };

    private static MeterMakeDto MapBoToDto(MeterMakeBo bo) => new()
    {
        Id = bo.Id,
        MeterMakeName = bo.MeterMakeName,
        CreatedBy = bo.CreatedBy,
        UpdatedBy = bo.UpdatedBy,
        CreatedAt = bo.CreatedAt,
        UpdatedAt = bo.UpdatedAt,
        IsActive = bo.IsActive
    };
}
