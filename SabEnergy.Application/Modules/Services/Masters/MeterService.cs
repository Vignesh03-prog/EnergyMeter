using FluentResults;
using SabEnergy.Application.Modules.BO.Masters;
using SabEnergy.Application.Modules.DTOs.Masters;
using SabEnergy.Application.Modules.Interfaces.Masters;
using SabEnergy.Domain.Modules.Masters;

namespace SabEnergy.Application.Modules.Services.Masters;

public class MeterService : IMeterService
{
    private readonly IMeterRepository _repository;

    public MeterService(IMeterRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<MeterDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAllAsync(cancellationToken);
        if (result.IsFailed)
            return Result.Fail<IEnumerable<MeterDto>>(result.Errors);

        var dtos = result.Value
            .Select(MapEntityToBo)
            .Select(MapBoToDto);

        return Result.Ok<IEnumerable<MeterDto>>(dtos);
    }

    public async Task<Result<MeterDto>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetByIdAsync(id, cancellationToken);
        if (result.IsFailed)
            return Result.Fail<MeterDto>(result.Errors);

        var bo = MapEntityToBo(result.Value);
        return Result.Ok(MapBoToDto(bo));
    }

    public async Task<Result<MeterDto>> CreateAsync(CreateMeterDto dto, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(dto.SerialNo))
            return Result.Fail<MeterDto>("Serial number is required.");

        if (dto.MeterMakeId <= 0)
            return Result.Fail<MeterDto>("Valid Meter Make ID is required.");

        var bo = new MeterBo
        {
            SerialNo = dto.SerialNo.Trim(),
            CustomName = dto.CustomName?.Trim() ?? string.Empty,
            ChannelId = dto.ChannelId,
            SlaveId = dto.SlaveId,
            DeviceProfileId = dto.DeviceProfileId,
            MeterMakeId = dto.MeterMakeId,
            MeterModelId = dto.MeterModelId > 0 ? dto.MeterModelId : null,
            Status = dto.Status?.Trim() ?? string.Empty,
            BaudRate = dto.BaudRate,
            ParityBit = dto.ParityBit?.Trim() ?? string.Empty,
            StopBit = dto.StopBit,
            DataBit = dto.DataBit,
            CreatedBy = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        var entity = MapBoToEntity(bo);
        var result = await _repository.CreateAsync(entity, cancellationToken);
        if (result.IsFailed)
            return Result.Fail<MeterDto>(result.Errors);

        var savedBo = MapEntityToBo(result.Value);
        return Result.Ok(MapBoToDto(savedBo));
    }

    public async Task<Result<MeterDto>> UpdateAsync(int id, UpdateMeterDto dto, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(dto.SerialNo))
            return Result.Fail<MeterDto>("Serial number is required.");

        if (dto.MeterMakeId <= 0)
            return Result.Fail<MeterDto>("Valid Meter Make ID is required.");

        var bo = new MeterBo
        {
            Id = id,
            SerialNo = dto.SerialNo.Trim(),
            CustomName = dto.CustomName?.Trim() ?? string.Empty,
            ChannelId = dto.ChannelId,
            SlaveId = dto.SlaveId,
            DeviceProfileId = dto.DeviceProfileId,
            MeterMakeId = dto.MeterMakeId,
            MeterModelId = dto.MeterModelId > 0 ? dto.MeterModelId : null,
            Status = dto.Status?.Trim() ?? string.Empty,
            BaudRate = dto.BaudRate,
            ParityBit = dto.ParityBit?.Trim() ?? string.Empty,
            StopBit = dto.StopBit,
            DataBit = dto.DataBit,
            IsActive = dto.IsActive,
            UpdatedBy = 1,
            UpdatedAt = DateTime.UtcNow
        };

        var entity = MapBoToEntity(bo);
        var result = await _repository.UpdateAsync(entity, cancellationToken);
        if (result.IsFailed)
            return Result.Fail<MeterDto>(result.Errors);

        var updatedBo = MapEntityToBo(result.Value);
        return Result.Ok(MapBoToDto(updatedBo));
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _repository.DeleteAsync(id, cancellationToken);
    }

    private static MeterBo MapEntityToBo(Meter entity) => new()
    {
        Id = entity.Id,
        SerialNo = entity.SerialNo,
        CustomName = entity.CustomName,
        ChannelId = entity.ChannelId,
        SlaveId = entity.SlaveId,
        DeviceProfileId = entity.DeviceProfileId,
        MeterMakeId = entity.MeterMakeId,
        MeterMakeName = entity.MeterMake?.MeterMakeName ?? string.Empty,
        MeterModelId = entity.MeterModelId,
        MeterModelName = entity.MeterModel?.MeterModelName,
        Status = entity.Status,
        BaudRate = entity.BaudRate,
        ParityBit = entity.ParityBit,
        StopBit = entity.StopBit,
        DataBit = entity.DataBit,
        CreatedBy = entity.CreatedBy,
        UpdatedBy = entity.UpdatedBy,
        CreatedAt = entity.CreatedAt,
        UpdatedAt = entity.UpdatedAt,
        IsActive = entity.IsActive,
        IsDeleted = entity.IsDeleted
    };

    private static Meter MapBoToEntity(MeterBo bo) => new()
    {
        Id = bo.Id,
        SerialNo = bo.SerialNo,
        CustomName = bo.CustomName,
        ChannelId = bo.ChannelId,
        SlaveId = bo.SlaveId,
        DeviceProfileId = bo.DeviceProfileId,
        MeterMakeId = bo.MeterMakeId,
        MeterModelId = bo.MeterModelId,
        Status = bo.Status,
        BaudRate = bo.BaudRate,
        ParityBit = bo.ParityBit,
        StopBit = bo.StopBit,
        DataBit = bo.DataBit,
        CreatedBy = bo.CreatedBy,
        UpdatedBy = bo.UpdatedBy,
        CreatedAt = bo.CreatedAt,
        UpdatedAt = bo.UpdatedAt,
        IsActive = bo.IsActive,
        IsDeleted = bo.IsDeleted
    };

    private static MeterDto MapBoToDto(MeterBo bo) => new()
    {
        Id = bo.Id,
        SerialNo = bo.SerialNo,
        CustomName = bo.CustomName,
        ChannelId = bo.ChannelId,
        SlaveId = bo.SlaveId,
        DeviceProfileId = bo.DeviceProfileId,
        MeterMakeId = bo.MeterMakeId,
        MeterMakeName = bo.MeterMakeName,
        MeterModelId = bo.MeterModelId,
        MeterModelName = bo.MeterModelName,
        Status = bo.Status,
        BaudRate = bo.BaudRate,
        ParityBit = bo.ParityBit,
        StopBit = bo.StopBit,
        DataBit = bo.DataBit,
        CreatedBy = bo.CreatedBy,
        UpdatedBy = bo.UpdatedBy,
        CreatedAt = bo.CreatedAt,
        UpdatedAt = bo.UpdatedAt,
        IsActive = bo.IsActive
    };
}
