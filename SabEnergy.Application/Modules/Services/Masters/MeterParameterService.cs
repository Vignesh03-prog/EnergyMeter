using FluentResults;
using SabEnergy.Application.Modules.BO.Masters;
using SabEnergy.Application.Modules.DTOs.Masters;
using SabEnergy.Application.Modules.Interfaces.Masters;
using SabEnergy.Domain.Modules.Masters;

namespace SabEnergy.Application.Modules.Services.Masters;

public class MeterParameterService : IMeterParameterService
{
    private readonly IMeterParameterRepository _repository;

    public MeterParameterService(IMeterParameterRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<MeterParameterDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAllAsync(cancellationToken);
        if (result.IsFailed)
            return Result.Fail<IEnumerable<MeterParameterDto>>(result.Errors);

        var dtos = result.Value
            .Select(MapEntityToBo)
            .Select(MapBoToDto);

        return Result.Ok<IEnumerable<MeterParameterDto>>(dtos);
    }

    public async Task<Result<MeterParameterDto>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetByIdAsync(id, cancellationToken);
        if (result.IsFailed)
            return Result.Fail<MeterParameterDto>(result.Errors);

        var bo = MapEntityToBo(result.Value);
        return Result.Ok(MapBoToDto(bo));
    }

    public async Task<Result<MeterParameterDto>> CreateAsync(CreateMeterParameterDto dto, CancellationToken cancellationToken = default)
    {
        if (dto.MeterProfileId <= 0)
            return Result.Fail<MeterParameterDto>("Valid MeterProfileId is required.");

        if (dto.ParameterMasterId <= 0)
            return Result.Fail<MeterParameterDto>("Valid ParameterMasterId is required.");

        var bo = new MeterParameterBo
        {
            MeterProfileId = dto.MeterProfileId,
            ParameterMasterId = dto.ParameterMasterId,
            Address = dto.Address,
            DataType = dto.DataType,
            Multiplier = dto.Multiplier,
            CreatedBy = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        var entity = MapBoToEntity(bo);
        var result = await _repository.CreateAsync(entity, cancellationToken);
        if (result.IsFailed)
            return Result.Fail<MeterParameterDto>(result.Errors);

        var savedBo = MapEntityToBo(result.Value);
        return Result.Ok(MapBoToDto(savedBo));
    }

    public async Task<Result<MeterParameterDto>> UpdateAsync(int id, UpdateMeterParameterDto dto, CancellationToken cancellationToken = default)
    {
        if (dto.MeterProfileId <= 0)
            return Result.Fail<MeterParameterDto>("Valid MeterProfileId is required.");

        if (dto.ParameterMasterId <= 0)
            return Result.Fail<MeterParameterDto>("Valid ParameterMasterId is required.");

        var bo = new MeterParameterBo
        {
            Id = id,
            MeterProfileId = dto.MeterProfileId,
            ParameterMasterId = dto.ParameterMasterId,
            Address = dto.Address,
            DataType = dto.DataType,
            Multiplier = dto.Multiplier,
            IsActive = dto.IsActive,
            UpdatedBy = 1,
            UpdatedAt = DateTime.UtcNow
        };

        var entity = MapBoToEntity(bo);
        var result = await _repository.UpdateAsync(entity, cancellationToken);
        if (result.IsFailed)
            return Result.Fail<MeterParameterDto>(result.Errors);

        var updatedBo = MapEntityToBo(result.Value);
        return Result.Ok(MapBoToDto(updatedBo));
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _repository.DeleteAsync(id, cancellationToken);
    }

    // Mapping Entity <-> BO
    private static MeterParameterBo MapEntityToBo(MeterParameter entity) => new()
    {
        Id = entity.Id,
        MeterProfileId = entity.MeterProfileId,
        ProfileVersion = entity.MeterProfile?.ProfileVersion ?? string.Empty,
        ParameterMasterId = entity.ParameterMasterId,
        Key = entity.ParameterMaster?.Key ?? string.Empty,
        DisplayName = entity.ParameterMaster?.DisplayName ?? string.Empty,
        CategoryId = entity.ParameterMaster?.CategoryId ?? 0,
        Unit = entity.ParameterMaster?.Unit ?? string.Empty,
        Address = entity.Address,
        DataType = entity.DataType,
        Multiplier = entity.Multiplier,
        CreatedBy = entity.CreatedBy,
        UpdatedBy = entity.UpdatedBy,
        CreatedAt = entity.CreatedAt,
        UpdatedAt = entity.UpdatedAt,
        IsActive = entity.IsActive,
        IsDeleted = entity.IsDeleted
    };

    private static MeterParameter MapBoToEntity(MeterParameterBo bo) => new()
    {
        Id = bo.Id,
        MeterProfileId = bo.MeterProfileId,
        ParameterMasterId = bo.ParameterMasterId,
        Address = bo.Address,
        DataType = bo.DataType,
        Multiplier = bo.Multiplier,
        CreatedBy = bo.CreatedBy,
        UpdatedBy = bo.UpdatedBy,
        CreatedAt = bo.CreatedAt,
        UpdatedAt = bo.UpdatedAt,
        IsActive = bo.IsActive,
        IsDeleted = bo.IsDeleted
    };

    // Mapping BO <-> DTO
    private static MeterParameterDto MapBoToDto(MeterParameterBo bo) => new()
    {
        Id = bo.Id,
        MeterProfileId = bo.MeterProfileId,
        ProfileVersion = bo.ProfileVersion,
        ParameterMasterId = bo.ParameterMasterId,
        Key = bo.Key,
        DisplayName = bo.DisplayName,
        CategoryId = bo.CategoryId,
        Unit = bo.Unit,
        Address = bo.Address,
        DataType = bo.DataType,
        Multiplier = bo.Multiplier,
        CreatedBy = bo.CreatedBy,
        UpdatedBy = bo.UpdatedBy,
        CreatedAt = bo.CreatedAt,
        UpdatedAt = bo.UpdatedAt,
        IsActive = bo.IsActive
    };
}
