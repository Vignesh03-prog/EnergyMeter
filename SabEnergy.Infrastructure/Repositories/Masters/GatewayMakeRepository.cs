using FluentResults;
using Microsoft.EntityFrameworkCore;
using SabEnergy.Application.Modules.Interfaces.Masters;
using SabEnergy.Domain.Modules.Masters;
using SabEnergy.Infrastructure.Persistence;

namespace SabEnergy.Infrastructure.Repositories.Masters;

public class GatewayMakeRepository : IGatewayMakeRepository
{
    private readonly ApplicationDbContext _context;

    public GatewayMakeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<GatewayMake>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var entities = await _context.GatewayMakes
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.Id)
                .ToListAsync(cancellationToken);

            return Result.Ok<IEnumerable<GatewayMake>>(entities);
        }
        catch (Exception ex)
        {
            return Result.Fail<IEnumerable<GatewayMake>>(ex.Message);
        }
    }

    public async Task<Result<GatewayMake>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await _context.GatewayMakes
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);

            if (entity == null)
                return Result.Fail<GatewayMake>($"Gateway make with ID {id} was not found.");

            return Result.Ok(entity);
        }
        catch (Exception ex)
        {
            return Result.Fail<GatewayMake>(ex.Message);
        }
    }

    public async Task<Result<GatewayMake>> CreateAsync(GatewayMake entity, CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.GatewayMakes.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Ok(entity);
        }
        catch (Exception ex)
        {
            return Result.Fail<GatewayMake>(ex.Message);
        }
    }

    public async Task<Result<GatewayMake>> UpdateAsync(GatewayMake entity, CancellationToken cancellationToken = default)
    {
        try
        {
            var existing = await _context.GatewayMakes
                .FirstOrDefaultAsync(x => x.Id == entity.Id && !x.IsDeleted, cancellationToken);

            if (existing == null)
                return Result.Fail<GatewayMake>($"Gateway make with ID {entity.Id} was not found.");

            existing.GatewayMakeName = entity.GatewayMakeName;
            existing.IsActive = entity.IsActive;
            existing.UpdatedBy = entity.UpdatedBy;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Ok(existing);
        }
        catch (Exception ex)
        {
            return Result.Fail<GatewayMake>(ex.Message);
        }
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var existing = await _context.GatewayMakes
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);

            if (existing == null)
                return Result.Fail($"Gateway make with ID {id} was not found.");

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
