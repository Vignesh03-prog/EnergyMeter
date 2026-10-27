using FluentResults;
using Microsoft.EntityFrameworkCore;
using SabEnergy.Application.Modules.Interfaces.Masters;
using SabEnergy.Domain.Modules.Masters;
using SabEnergy.Infrastructure.Persistence;

namespace SabEnergy.Infrastructure.Repositories.Masters;

public class ChannelRepository : IChannelRepository
{
    private readonly ApplicationDbContext _context;

    public ChannelRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<Channel>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var entities = await _context.Channels
                .Include(x => x.Gateway)
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.Id)
                .ToListAsync(cancellationToken);

            return Result.Ok<IEnumerable<Channel>>(entities);
        }
        catch (Exception ex)
        {
            return Result.Fail<IEnumerable<Channel>>(ex.Message);
        }
    }

    public async Task<Result<Channel>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await _context.Channels
                .Include(x => x.Gateway)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);

            if (entity == null)
                return Result.Fail<Channel>($"Channel with ID {id} was not found.");

            return Result.Ok(entity);
        }
        catch (Exception ex)
        {
            return Result.Fail<Channel>(ex.Message);
        }
    }

    public async Task<Result<IEnumerable<Channel>>> GetByGatewayIdAsync(int gatewayId, CancellationToken cancellationToken = default)
    {
        try
        {
            var entities = await _context.Channels
                .Include(x => x.Gateway)
                .AsNoTracking()
                .Where(x => x.GatewayId == gatewayId && !x.IsDeleted)
                .OrderByDescending(x => x.Id)
                .ToListAsync(cancellationToken);

            return Result.Ok<IEnumerable<Channel>>(entities);
        }
        catch (Exception ex)
        {
            return Result.Fail<IEnumerable<Channel>>(ex.Message);
        }
    }

    public async Task<Result<Channel>> CreateAsync(Channel entity, CancellationToken cancellationToken = default)
    {
        try
        {
            var gatewayExists = await _context.Gateways
                .AnyAsync(x => x.Id == entity.GatewayId && !x.IsDeleted, cancellationToken);

            if (!gatewayExists)
                return Result.Fail<Channel>($"Gateway with ID {entity.GatewayId} was not found.");

            await _context.Channels.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            // Load the navigation property for mapping
            await _context.Entry(entity).Reference(x => x.Gateway).LoadAsync(cancellationToken);

            return Result.Ok(entity);
        }
        catch (Exception ex)
        {
            return Result.Fail<Channel>(ex.Message);
        }
    }

    public async Task<Result<Channel>> UpdateAsync(Channel entity, CancellationToken cancellationToken = default)
    {
        try
        {
            var existing = await _context.Channels
                .Include(x => x.Gateway)
                .FirstOrDefaultAsync(x => x.Id == entity.Id && !x.IsDeleted, cancellationToken);

            if (existing == null)
                return Result.Fail<Channel>($"Channel with ID {entity.Id} was not found.");

            var gatewayExists = await _context.Gateways
                .AnyAsync(x => x.Id == entity.GatewayId && !x.IsDeleted, cancellationToken);

            if (!gatewayExists)
                return Result.Fail<Channel>($"Gateway with ID {entity.GatewayId} was not found.");

            existing.ChannelName = entity.ChannelName;
            existing.GatewayId = entity.GatewayId;
            existing.IsActive = entity.IsActive;
            existing.UpdatedBy = entity.UpdatedBy;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Ok(existing);
        }
        catch (Exception ex)
        {
            return Result.Fail<Channel>(ex.Message);
        }
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var existing = await _context.Channels
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);

            if (existing == null)
                return Result.Fail($"Channel with ID {id} was not found.");

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

