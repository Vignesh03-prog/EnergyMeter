using FluentResults;
using SabEnergy.Application.Modules.BO.Masters;
using SabEnergy.Application.Modules.DTOs.Masters;
using SabEnergy.Application.Modules.Interfaces.Masters;
using SabEnergy.Domain.Modules.Masters;

namespace SabEnergy.Application.Modules.Services.Masters;

public class MeterProfileService : IMeterProfileService
{
    private readonly IMeterProfileRepository _repository;

    public MeterProfileService(IMeterProfileRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<MeterProfileDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAllAsync(cancellationToken);
        if (result.IsFailed)
            return Result.Fail<IEnumerable<MeterProfileDto>>(result.Errors);

        var dtos = result.Value
            .Select(MapEntityToBo)
            .Select(MapBoToDto);

        return Result.Ok<IEnumerable<MeterProfileDto>>(dtos);
    }

    public async Task<Result<MeterProfileDto>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetByIdAsync(id, cancellationToken);
        if (result.IsFailed)
            return Result.Fail<MeterProfileDto>(result.Errors);

        var bo = MapEntityToBo(result.Value);
        return Result.Ok(MapBoToDto(bo));
    }

    public async Task<Result<MeterProfileDto>> CreateAsync(CreateMeterProfileDto dto, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(dto.ProfileVersion))
            return Result.Fail<MeterProfileDto>("Profile version is required.");

        if (dto.MakeId <= 0)
            return Result.Fail<MeterProfileDto>("Valid Make ID is required.");

        if (dto.ModelId <= 0)
            return Result.Fail<MeterProfileDto>("Valid Model ID is required.");

        var bo = new MeterProfileBo
        {
            ProfileVersion = dto.ProfileVersion.Trim(),
            MakeId = dto.MakeId,
            ModelId = dto.ModelId,
            AddressOffest = dto.AddressOffest,
            HoldingRegister = dto.HoldingRegister,
            ByteOrder = dto.ByteOrder,
            CreatedBy = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        var entity = MapBoToEntity(bo);
        var result = await _repository.CreateAsync(entity, cancellationToken);
        if (result.IsFailed)
            return Result.Fail<MeterProfileDto>(result.Errors);

        var savedBo = MapEntityToBo(result.Value);
        return Result.Ok(MapBoToDto(savedBo));
    }

    public async Task<Result<MeterProfileDto>> UpdateAsync(int id, UpdateMeterProfileDto dto, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(dto.ProfileVersion))
            return Result.Fail<MeterProfileDto>("Profile version is required.");

        if (dto.MakeId <= 0)
            return Result.Fail<MeterProfileDto>("Valid Make ID is required.");

        if (dto.ModelId <= 0)
            return Result.Fail<MeterProfileDto>("Valid Model ID is required.");

        var bo = new MeterProfileBo
        {
            Id = id,
            ProfileVersion = dto.ProfileVersion.Trim(),
            MakeId = dto.MakeId,
            ModelId = dto.ModelId,
            AddressOffest = dto.AddressOffest,
            HoldingRegister = dto.HoldingRegister,
            ByteOrder = dto.ByteOrder,
            IsActive = dto.IsActive,
            UpdatedBy = 1,
            UpdatedAt = DateTime.UtcNow
        };

        var entity = MapBoToEntity(bo);
        var result = await _repository.UpdateAsync(entity, cancellationToken);
        if (result.IsFailed)
            return Result.Fail<MeterProfileDto>(result.Errors);

        var updatedBo = MapEntityToBo(result.Value);
        return Result.Ok(MapBoToDto(updatedBo));
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _repository.DeleteAsync(id, cancellationToken);
    }

    // Mapping Entity <-> BO
    private static MeterProfileBo MapEntityToBo(MeterProfile entity) => new()
    {
        Id = entity.Id,
        ProfileVersion = entity.ProfileVersion,
        MakeId = entity.MakeId,
        MakeName = entity.MeterMake?.MeterMakeName ?? string.Empty,
        ModelId = entity.ModelId,
        ModelName = entity.MeterModel?.MeterModelName ?? string.Empty,
        AddressOffest = entity.AddressOffest,
        HoldingRegister = entity.HoldingRegister,
        ByteOrder = entity.ByteOrder,
        CreatedBy = entity.CreatedBy,
        UpdatedBy = entity.UpdatedBy,
        CreatedAt = entity.CreatedAt,
        UpdatedAt = entity.UpdatedAt,
        IsActive = entity.IsActive,
        IsDeleted = entity.IsDeleted
    };

    private static MeterProfile MapBoToEntity(MeterProfileBo bo) => new()
    {
        Id = bo.Id,
        ProfileVersion = bo.ProfileVersion,
        MakeId = bo.MakeId,
        ModelId = bo.ModelId,
        AddressOffest = bo.AddressOffest,
        HoldingRegister = bo.HoldingRegister,
        ByteOrder = bo.ByteOrder,
        CreatedBy = bo.CreatedBy,
        UpdatedBy = bo.UpdatedBy,
        CreatedAt = bo.CreatedAt,
        UpdatedAt = bo.UpdatedAt,
        IsActive = bo.IsActive,
        IsDeleted = bo.IsDeleted
    };

    // Mapping BO <-> DTO
    private static MeterProfileDto MapBoToDto(MeterProfileBo bo) => new()
    {
        Id = bo.Id,
        ProfileVersion = bo.ProfileVersion,
        MakeId = bo.MakeId,
        MakeName = bo.MakeName,
        ModelId = bo.ModelId,
        ModelName = bo.ModelName,
        AddressOffest = bo.AddressOffest,
        HoldingRegister = bo.HoldingRegister,
        ByteOrder = bo.ByteOrder,
        CreatedBy = bo.CreatedBy,
        UpdatedBy = bo.UpdatedBy,
        CreatedAt = bo.CreatedAt,
        UpdatedAt = bo.UpdatedAt,
        IsActive = bo.IsActive
    };
}
