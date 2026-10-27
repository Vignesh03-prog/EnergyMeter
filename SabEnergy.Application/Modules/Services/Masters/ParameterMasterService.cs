using FluentResults;
using SabEnergy.Application.Modules.BO.Masters;
using SabEnergy.Application.Modules.DTOs.Masters;
using SabEnergy.Application.Modules.Interfaces.Masters;
using SabEnergy.Domain.Modules.Masters;

namespace SabEnergy.Application.Modules.Services.Masters;

public class ParameterMasterService : IParameterMasterService
{
    private readonly IParameterMasterRepository _repository;

    public ParameterMasterService(IParameterMasterRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<ParameterMasterDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAllAsync(cancellationToken);
        if (result.IsFailed)
            return Result.Fail<IEnumerable<ParameterMasterDto>>(result.Errors);

        var dtos = result.Value
            .Select(MapEntityToBo)
            .Select(MapBoToDto);

        return Result.Ok<IEnumerable<ParameterMasterDto>>(dtos);
    }

    public async Task<Result<ParameterMasterDto>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetByIdAsync(id, cancellationToken);
        if (result.IsFailed)
            return Result.Fail<ParameterMasterDto>(result.Errors);

        var bo = MapEntityToBo(result.Value);
        return Result.Ok(MapBoToDto(bo));
    }

    public async Task<Result<ParameterMasterDto>> CreateAsync(CreateParameterMasterDto dto, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(dto.Key))
            return Result.Fail<ParameterMasterDto>("Key is required.");

        if (string.IsNullOrWhiteSpace(dto.DisplayName))
            return Result.Fail<ParameterMasterDto>("DisplayName is required.");

        var bo = new ParameterMasterBo
        {
            Key = dto.Key.Trim(),
            DisplayName = dto.DisplayName.Trim(),
            CategoryId = dto.CategoryId,
            Unit = dto.Unit?.Trim() ?? string.Empty,
            CreatedBy = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        var entity = MapBoToEntity(bo);
        var result = await _repository.CreateAsync(entity, cancellationToken);
        if (result.IsFailed)
            return Result.Fail<ParameterMasterDto>(result.Errors);

        var savedBo = MapEntityToBo(result.Value);
        return Result.Ok(MapBoToDto(savedBo));
    }

    public async Task<Result<ParameterMasterDto>> UpdateAsync(int id, UpdateParameterMasterDto dto, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(dto.Key))
            return Result.Fail<ParameterMasterDto>("Key is required.");

        if (string.IsNullOrWhiteSpace(dto.DisplayName))
            return Result.Fail<ParameterMasterDto>("DisplayName is required.");

        var bo = new ParameterMasterBo
        {
            Id = id,
            Key = dto.Key.Trim(),
            DisplayName = dto.DisplayName.Trim(),
            CategoryId = dto.CategoryId,
            Unit = dto.Unit?.Trim() ?? string.Empty,
            IsActive = dto.IsActive,
            UpdatedBy = 1,
            UpdatedAt = DateTime.UtcNow
        };

        var entity = MapBoToEntity(bo);
        var result = await _repository.UpdateAsync(entity, cancellationToken);
        if (result.IsFailed)
            return Result.Fail<ParameterMasterDto>(result.Errors);

        var updatedBo = MapEntityToBo(result.Value);
        return Result.Ok(MapBoToDto(updatedBo));
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _repository.DeleteAsync(id, cancellationToken);
    }

    // Mapping Entity <-> BO
    private static ParameterMasterBo MapEntityToBo(ParameterMaster entity) => new()
    {
        Id = entity.Id,
        Key = entity.Key,
        DisplayName = entity.DisplayName,
        CategoryId = entity.CategoryId,
        Unit = entity.Unit,
        CreatedBy = entity.CreatedBy,
        UpdatedBy = entity.UpdatedBy,
        CreatedAt = entity.CreatedAt,
        UpdatedAt = entity.UpdatedAt,
        IsActive = entity.IsActive,
        IsDeleted = entity.IsDeleted
    };

    private static ParameterMaster MapBoToEntity(ParameterMasterBo bo) => new()
    {
        Id = bo.Id,
        Key = bo.Key,
        DisplayName = bo.DisplayName,
        CategoryId = bo.CategoryId,
        Unit = bo.Unit,
        CreatedBy = bo.CreatedBy,
        UpdatedBy = bo.UpdatedBy,
        CreatedAt = bo.CreatedAt,
        UpdatedAt = bo.UpdatedAt,
        IsActive = bo.IsActive,
        IsDeleted = bo.IsDeleted
    };

    // Mapping BO <-> DTO
    private static ParameterMasterDto MapBoToDto(ParameterMasterBo bo) => new()
    {
        Id = bo.Id,
        Key = bo.Key,
        DisplayName = bo.DisplayName,
        CategoryId = bo.CategoryId,
        Unit = bo.Unit,
        CreatedBy = bo.CreatedBy,
        UpdatedBy = bo.UpdatedBy,
        CreatedAt = bo.CreatedAt,
        UpdatedAt = bo.UpdatedAt,
        IsActive = bo.IsActive
    };
}
