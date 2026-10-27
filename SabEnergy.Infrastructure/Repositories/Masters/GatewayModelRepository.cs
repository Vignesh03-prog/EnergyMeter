using FluentResults;
using Microsoft.EntityFrameworkCore;
using SabEnergy.Application.Modules.Interfaces.Masters;
using SabEnergy.Domain.Modules.Masters.Model;
using SabEnergy.Infrastructure.Persistence;

namespace SabEnergy.Infrastructure.Repositories.Masters;

public class GatewayModelRepository : IGatewayModelRepository
{
    private readonly ApplicationDbContext _context;

    public GatewayModelRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<GatewayModel>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var entities = await _context.GatewayModels
                .Include(x => x.GatewayMake)
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.Id)
                .ToListAsync(cancellationToken);

            return Result.Ok<IEnumerable<GatewayModel>>(entities);
        }
        catch (Exception ex)
        {
            return Result.Fail<IEnumerable<GatewayModel>>(ex.Message);
        }
    }

    public async Task<Result<GatewayModel>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await _context.GatewayModels
                .Include(x => x.GatewayMake)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);

            if (entity == null)
                return Result.Fail<GatewayModel>($"Gateway model with ID {id} was not found.");

            return Result.Ok(entity);
        }
        catch (Exception ex)
        {
            return Result.Fail<GatewayModel>(ex.Message);
        }
    }

    public async Task<Result<GatewayModel>> CreateAsync(GatewayModel entity, CancellationToken cancellationToken = default)
    {
        try
        {
            var makeExists = await _context.GatewayMakes
                .AnyAsync(x => x.Id == entity.GatewayMakeId && !x.IsDeleted, cancellationToken);

            if (!makeExists)
                return Result.Fail<GatewayModel>($"Gateway make with ID {entity.GatewayMakeId} was not found.");

            await _context.GatewayModels.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            // Load the navigation property for mapping
            await _context.Entry(entity).Reference(x => x.GatewayMake).LoadAsync(cancellationToken);

            return Result.Ok(entity);
        }
        catch (Exception ex)
        {
            return Result.Fail<GatewayModel>(ex.Message);
        }
    }

    public async Task<Result<GatewayModel>> UpdateAsync(GatewayModel entity, CancellationToken cancellationToken = default)
    {
        try
        {
            var existing = await _context.GatewayModels
                .Include(x => x.GatewayMake)
                .FirstOrDefaultAsync(x => x.Id == entity.Id && !x.IsDeleted, cancellationToken);

            if (existing == null)
                return Result.Fail<GatewayModel>($"Gateway model with ID {entity.Id} was not found.");

            var makeExists = await _context.GatewayMakes
                .AnyAsync(x => x.Id == entity.GatewayMakeId && !x.IsDeleted, cancellationToken);

            if (!makeExists)
                return Result.Fail<GatewayModel>($"Gateway make with ID {entity.GatewayMakeId} was not found.");

            existing.GatewayModelName = entity.GatewayModelName;
            existing.GatewayMakeId = entity.GatewayMakeId;
            existing.IsActive = entity.IsActive;
            existing.UpdatedBy = entity.UpdatedBy;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Ok(existing);
        }
        catch (Exception ex)
        {
            return Result.Fail<GatewayModel>(ex.Message);
        }
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var existing = await _context.GatewayModels
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);

            if (existing == null)
                return Result.Fail($"Gateway model with ID {id} was not found.");

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
