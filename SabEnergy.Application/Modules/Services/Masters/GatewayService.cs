using FluentResults;
using SabEnergy.Application.Modules.BO.Masters;
using SabEnergy.Application.Modules.DTOs.Masters;
using SabEnergy.Application.Modules.Interfaces.Masters;
using SabEnergy.Domain.Modules.Masters;

namespace SabEnergy.Application.Modules.Services.Masters;

public class GatewayService : IGatewayService
{
    private readonly IGatewayRepository _repository;

    public GatewayService(IGatewayRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<GatewayDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAllAsync(cancellationToken);
        if (result.IsFailed)
            return Result.Fail<IEnumerable<GatewayDto>>(result.Errors);

        var dtos = result.Value
            .Select(MapEntityToBo)
            .Select(MapBoToDto);

        return Result.Ok<IEnumerable<GatewayDto>>(dtos);
    }

    public async Task<Result<GatewayDto>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetByIdAsync(id, cancellationToken);
        if (result.IsFailed)
            return Result.Fail<GatewayDto>(result.Errors);

        var bo = MapEntityToBo(result.Value);
        return Result.Ok(MapBoToDto(bo));
    }

    public async Task<Result<GatewayDto>> CreateAsync(CreateGatewayDto dto, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(dto.DeviceName))
            return Result.Fail<GatewayDto>("Device name is required.");

        if (string.IsNullOrWhiteSpace(dto.SerialNo))
            return Result.Fail<GatewayDto>("Serial number is required.");

        if (dto.GatewayMakeId <= 0)
            return Result.Fail<GatewayDto>("Valid Gateway Make ID is required.");

        var bo = new GatewayBo
        {
            DeviceName = dto.DeviceName.Trim(),
            SerialNo = dto.SerialNo.Trim(),
            GatewayMakeId = dto.GatewayMakeId,
            GatewayModelId = dto.GatewayModelId > 0 ? dto.GatewayModelId : null,
            IpAddress = dto.IpAddress?.Trim() ?? string.Empty,
            MacAddress = dto.MacAddress?.Trim() ?? string.Empty,
            ChannelCount = dto.ChannelCount,
            FirmwareVersion = dto.FirmwareVersion?.Trim(),
            KeepaliveTime = dto.KeepaliveTime,
            Wifi_SSID = dto.Wifi_SSID?.Trim() ?? string.Empty,
            Wifi_Port = dto.Wifi_Port,
            CreatedBy = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        var entity = MapBoToEntity(bo);
        var result = await _repository.CreateAsync(entity, cancellationToken);
        if (result.IsFailed)
            return Result.Fail<GatewayDto>(result.Errors);

        var savedBo = MapEntityToBo(result.Value);
        return Result.Ok(MapBoToDto(savedBo));
    }

    public async Task<Result<GatewayDto>> UpdateAsync(int id, UpdateGatewayDto dto, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(dto.DeviceName))
            return Result.Fail<GatewayDto>("Device name is required.");

        if (string.IsNullOrWhiteSpace(dto.SerialNo))
            return Result.Fail<GatewayDto>("Serial number is required.");

        if (dto.GatewayMakeId <= 0)
            return Result.Fail<GatewayDto>("Valid Gateway Make ID is required.");

        var bo = new GatewayBo
        {
            Id = id,
            DeviceName = dto.DeviceName.Trim(),
            SerialNo = dto.SerialNo.Trim(),
            GatewayMakeId = dto.GatewayMakeId,
            GatewayModelId = dto.GatewayModelId > 0 ? dto.GatewayModelId : null,
            IpAddress = dto.IpAddress?.Trim() ?? string.Empty,
            MacAddress = dto.MacAddress?.Trim() ?? string.Empty,
            ChannelCount = dto.ChannelCount,
            FirmwareVersion = dto.FirmwareVersion?.Trim(),
            KeepaliveTime = dto.KeepaliveTime,
            Wifi_SSID = dto.Wifi_SSID?.Trim() ?? string.Empty,
            Wifi_Port = dto.Wifi_Port,
            IsActive = dto.IsActive,
            UpdatedBy = 1,
            UpdatedAt = DateTime.UtcNow
        };

        var entity = MapBoToEntity(bo);
        var result = await _repository.UpdateAsync(entity, cancellationToken);
        if (result.IsFailed)
            return Result.Fail<GatewayDto>(result.Errors);

        var updatedBo = MapEntityToBo(result.Value);
        return Result.Ok(MapBoToDto(updatedBo));
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _repository.DeleteAsync(id, cancellationToken);
    }

    private static GatewayBo MapEntityToBo(Gateway entity) => new()
    {
        Id = entity.Id,
        DeviceName = entity.DeviceName,
        SerialNo = entity.SerialNo,
        GatewayMakeId = entity.GatewayMakeId,
        GatewayMakeName = entity.GatewayMake?.GatewayMakeName ?? string.Empty,
        GatewayModelId = entity.GatewayModelId,
        GatewayModelName = entity.GatewayModel?.GatewayModelName,
        IpAddress = entity.IpAddress,
        MacAddress = entity.MacAddress,
        ChannelCount = entity.ChannelCount,
        FirmwareVersion = entity.FirmwareVersion,
        KeepaliveTime = entity.KeepaliveTime,
        Wifi_SSID = entity.Wifi_SSID,
        Wifi_Port = entity.Wifi_Port,
        CreatedBy = entity.CreatedBy,
        UpdatedBy = entity.UpdatedBy,
        CreatedAt = entity.CreatedAt,
        UpdatedAt = entity.UpdatedAt,
        IsActive = entity.IsActive,
        IsDeleted = entity.IsDeleted
    };

    private static Gateway MapBoToEntity(GatewayBo bo) => new()
    {
        Id = bo.Id,
        DeviceName = bo.DeviceName,
        SerialNo = bo.SerialNo,
        GatewayMakeId = bo.GatewayMakeId,
        GatewayModelId = bo.GatewayModelId,
        IpAddress = bo.IpAddress,
        MacAddress = bo.MacAddress,
        ChannelCount = bo.ChannelCount,
        FirmwareVersion = bo.FirmwareVersion,
        KeepaliveTime = bo.KeepaliveTime,
        Wifi_SSID = bo.Wifi_SSID,
        Wifi_Port = bo.Wifi_Port,
        CreatedBy = bo.CreatedBy,
        UpdatedBy = bo.UpdatedBy,
        CreatedAt = bo.CreatedAt,
        UpdatedAt = bo.UpdatedAt,
        IsActive = bo.IsActive,
        IsDeleted = bo.IsDeleted
    };

    private static GatewayDto MapBoToDto(GatewayBo bo) => new()
    {
        Id = bo.Id,
        DeviceName = bo.DeviceName,
        SerialNo = bo.SerialNo,
        GatewayMakeId = bo.GatewayMakeId,
        GatewayMakeName = bo.GatewayMakeName,
        GatewayModelId = bo.GatewayModelId,
        GatewayModelName = bo.GatewayModelName,
        IpAddress = bo.IpAddress,
        MacAddress = bo.MacAddress,
        ChannelCount = bo.ChannelCount,
        FirmwareVersion = bo.FirmwareVersion,
        KeepaliveTime = bo.KeepaliveTime,
        Wifi_SSID = bo.Wifi_SSID,
        Wifi_Port = bo.Wifi_Port,
        CreatedBy = bo.CreatedBy,
        UpdatedBy = bo.UpdatedBy,
        CreatedAt = bo.CreatedAt,
        UpdatedAt = bo.UpdatedAt,
        IsActive = bo.IsActive
    };
}
