using FluentResults;
using Microsoft.EntityFrameworkCore;
using SabEnergy.Application.Modules.Interfaces.Masters;
using SabEnergy.Domain.Modules.Masters;
using SabEnergy.Infrastructure.Persistence;

namespace SabEnergy.Infrastructure.Repositories.Masters;

public class GatewayRepository : IGatewayRepository
{
    private readonly ApplicationDbContext _context;

    public GatewayRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<Gateway>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var entities = await _context.Gateways
                .Include(x => x.GatewayMake)
                .Include(x => x.GatewayModel)
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.Id)
                .ToListAsync(cancellationToken);

            return Result.Ok<IEnumerable<Gateway>>(entities);
        }
        catch (Exception ex)
        {
            return Result.Fail<IEnumerable<Gateway>>(ex.Message);
        }
    }

    public async Task<Result<Gateway>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await _context.Gateways
                .Include(x => x.GatewayMake)
                .Include(x => x.GatewayModel)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);

            if (entity == null)
                return Result.Fail<Gateway>($"Gateway with ID {id} was not found.");

            return Result.Ok(entity);
        }
        catch (Exception ex)
        {
            return Result.Fail<Gateway>(ex.Message);
        }
    }

    public async Task<Result<Gateway>> CreateAsync(Gateway entity, CancellationToken cancellationToken = default)
    {
        try
        {
            var makeExists = await _context.GatewayMakes
                .AnyAsync(x => x.Id == entity.GatewayMakeId && !x.IsDeleted, cancellationToken);

            if (!makeExists)
                return Result.Fail<Gateway>($"Gateway make with ID {entity.GatewayMakeId} was not found.");

            if (entity.GatewayModelId.HasValue)
            {
                var modelExists = await _context.GatewayModels
                    .AnyAsync(x => x.Id == entity.GatewayModelId.Value && !x.IsDeleted, cancellationToken);

                if (!modelExists)
                    return Result.Fail<Gateway>($"Gateway model with ID {entity.GatewayModelId.Value} was not found.");
            }

            await _context.Gateways.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            await _context.Entry(entity).Reference(x => x.GatewayMake).LoadAsync(cancellationToken);
            if (entity.GatewayModelId.HasValue)
            {
                await _context.Entry(entity).Reference(x => x.GatewayModel).LoadAsync(cancellationToken);
            }

            return Result.Ok(entity);
        }
        catch (Exception ex)
        {
            return Result.Fail<Gateway>(ex.Message);
        }
    }

    public async Task<Result<Gateway>> UpdateAsync(Gateway entity, CancellationToken cancellationToken = default)
    {
        try
        {
            var existing = await _context.Gateways
                .Include(x => x.GatewayMake)
                .Include(x => x.GatewayModel)
                .FirstOrDefaultAsync(x => x.Id == entity.Id && !x.IsDeleted, cancellationToken);

            if (existing == null)
                return Result.Fail<Gateway>($"Gateway with ID {entity.Id} was not found.");

            var makeExists = await _context.GatewayMakes
                .AnyAsync(x => x.Id == entity.GatewayMakeId && !x.IsDeleted, cancellationToken);

            if (!makeExists)
                return Result.Fail<Gateway>($"Gateway make with ID {entity.GatewayMakeId} was not found.");

            if (entity.GatewayModelId.HasValue)
            {
                var modelExists = await _context.GatewayModels
                    .AnyAsync(x => x.Id == entity.GatewayModelId.Value && !x.IsDeleted, cancellationToken);

                if (!modelExists)
                    return Result.Fail<Gateway>($"Gateway model with ID {entity.GatewayModelId.Value} was not found.");
            }

            existing.DeviceName = entity.DeviceName;
            existing.SerialNo = entity.SerialNo;
            existing.GatewayMakeId = entity.GatewayMakeId;
            existing.GatewayModelId = entity.GatewayModelId;
            existing.IpAddress = entity.IpAddress;
            existing.MacAddress = entity.MacAddress;
            existing.ChannelCount = entity.ChannelCount;
            existing.FirmwareVersion = entity.FirmwareVersion;
            existing.KeepaliveTime = entity.KeepaliveTime;
            existing.Wifi_SSID = entity.Wifi_SSID;
            existing.Wifi_Port = entity.Wifi_Port;
            existing.IsActive = entity.IsActive;
            existing.UpdatedBy = entity.UpdatedBy;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Ok(existing);
        }
        catch (Exception ex)
        {
            return Result.Fail<Gateway>(ex.Message);
        }
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var existing = await _context.Gateways
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);

            if (existing == null)
                return Result.Fail($"Gateway with ID {id} was not found.");

            existing.IsDeleted = true;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}
