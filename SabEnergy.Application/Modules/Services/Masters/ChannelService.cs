using FluentResults;
using SabEnergy.Application.Modules.BO.Masters;
using SabEnergy.Application.Modules.DTOs.Masters;
using SabEnergy.Application.Modules.Interfaces.Masters;
using SabEnergy.Domain.Modules.Masters;

namespace SabEnergy.Application.Modules.Services.Masters;

public class ChannelService : IChannelService
{
    private readonly IChannelRepository _repository;

    public ChannelService(IChannelRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<ChannelDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAllAsync(cancellationToken);
        if (result.IsFailed)
            return Result.Fail<IEnumerable<ChannelDto>>(result.Errors);

        // Map Entity -> BO -> DTO
        var dtos = result.Value
            .Select(MapEntityToBo)
            .Select(MapBoToDto);

        return Result.Ok<IEnumerable<ChannelDto>>(dtos);
    }

    public async Task<Result<ChannelDto>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetByIdAsync(id, cancellationToken);
        if (result.IsFailed)
            return Result.Fail<ChannelDto>(result.Errors);

        var bo = MapEntityToBo(result.Value);
        return Result.Ok(MapBoToDto(bo));
    }

    public async Task<Result<IEnumerable<ChannelDto>>> GetByGatewayIdAsync(int gatewayId, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetByGatewayIdAsync(gatewayId, cancellationToken);
        if (result.IsFailed)
            return Result.Fail<IEnumerable<ChannelDto>>(result.Errors);

        var dtos = result.Value
            .Select(MapEntityToBo)
            .Select(MapBoToDto);

        return Result.Ok<IEnumerable<ChannelDto>>(dtos);
    }

    public async Task<Result<ChannelDto>> CreateAsync(CreateChannelDto dto, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(dto.ChannelName))
            return Result.Fail<ChannelDto>("Channel name is required.");

        if (dto.GatewayId <= 0)
            return Result.Fail<ChannelDto>("Valid Gateway ID is required.");

        // Map DTO -> BO -> Entity
        var bo = new ChannelBo
        {
            ChannelName = dto.ChannelName.Trim(),
            GatewayId = dto.GatewayId,
            CreatedBy = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsActive = true,
            IsDeleted = false
        };

        var entity = MapBoToEntity(bo);
        var result = await _repository.CreateAsync(entity, cancellationToken);
        if (result.IsFailed)
            return Result.Fail<ChannelDto>(result.Errors);

        var savedBo = MapEntityToBo(result.Value);
        return Result.Ok(MapBoToDto(savedBo));
    }

    public async Task<Result<ChannelDto>> UpdateAsync(int id, UpdateChannelDto dto, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(dto.ChannelName))
            return Result.Fail<ChannelDto>("Channel name is required.");

        if (dto.GatewayId <= 0)
            return Result.Fail<ChannelDto>("Valid Gateway ID is required.");

        // Map DTO -> BO -> Entity
        var bo = new ChannelBo
        {
            Id = id,
            ChannelName = dto.ChannelName.Trim(),
            GatewayId = dto.GatewayId,
            IsActive = dto.IsActive,
            UpdatedBy = 1,
            UpdatedAt = DateTime.UtcNow
        };

        var entity = MapBoToEntity(bo);
        var result = await _repository.UpdateAsync(entity, cancellationToken);
        if (result.IsFailed)
            return Result.Fail<ChannelDto>(result.Errors);

        var updatedBo = MapEntityToBo(result.Value);
        return Result.Ok(MapBoToDto(updatedBo));
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _repository.DeleteAsync(id, cancellationToken);
    }

    // Mapping Entity <-> BO
    private static ChannelBo MapEntityToBo(Channel entity) => new()
    {
        Id = entity.Id,
        ChannelName = entity.ChannelName,
        GatewayId = entity.GatewayId,
        GatewayDeviceName = entity.Gateway?.DeviceName ?? string.Empty,
        CreatedBy = entity.CreatedBy,
        UpdatedBy = entity.UpdatedBy,
        CreatedAt = entity.CreatedAt,
        UpdatedAt = entity.UpdatedAt,
        IsActive = entity.IsActive,
        IsDeleted = entity.IsDeleted
    };

    private static Channel MapBoToEntity(ChannelBo bo) => new()
    {
        Id = bo.Id,
        ChannelName = bo.ChannelName,
        GatewayId = bo.GatewayId,
        CreatedBy = bo.CreatedBy,
        UpdatedBy = bo.UpdatedBy,
        CreatedAt = bo.CreatedAt,
        UpdatedAt = bo.UpdatedAt,
        IsActive = bo.IsActive,
        IsDeleted = bo.IsDeleted
    };

    // Mapping BO <-> DTO
    private static ChannelDto MapBoToDto(ChannelBo bo) => new()
    {
        Id = bo.Id,
        ChannelName = bo.ChannelName,
        GatewayId = bo.GatewayId,
        GatewayDeviceName = bo.GatewayDeviceName,
        CreatedBy = bo.CreatedBy,
        UpdatedBy = bo.UpdatedBy,
        CreatedAt = bo.CreatedAt,
        UpdatedAt = bo.UpdatedAt,
        IsActive = bo.IsActive
    };
}

