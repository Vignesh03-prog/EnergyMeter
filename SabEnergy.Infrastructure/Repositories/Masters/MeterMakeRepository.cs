using FluentResults;
using Microsoft.EntityFrameworkCore;
using SabEnergy.Application.Modules.Interfaces.Masters;
using SabEnergy.Domain.Modules.Masters;
using SabEnergy.Infrastructure.Persistence;

namespace SabEnergy.Infrastructure.Repositories.Masters;

public class MeterMakeRepository : IMeterMakeRepository
{
    private readonly ApplicationDbContext _context;

    public MeterMakeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<MeterMake>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var entities = await _context.MeterMakes
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.Id)
                .ToListAsync(cancellationToken);

            return Result.Ok<IEnumerable<MeterMake>>(entities);
        }
        catch (Exception ex)
        {
            return Result.Fail<IEnumerable<MeterMake>>(ex.Message);
        }
    }

    public async Task<Result<MeterMake>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await _context.MeterMakes
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);

            if (entity == null)
                return Result.Fail<MeterMake>($"Meter make with ID {id} was not found.");

            return Result.Ok(entity);
        }
        catch (Exception ex)
        {
            return Result.Fail<MeterMake>(ex.Message);
        }
    }

    public async Task<Result<MeterMake>> CreateAsync(MeterMake entity, CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.MeterMakes.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Ok(entity);
        }
        catch (Exception ex)
        {
            return Result.Fail<MeterMake>(ex.Message);
        }
    }

    public async Task<Result<MeterMake>> UpdateAsync(MeterMake entity, CancellationToken cancellationToken = default)
    {
        try
        {
            var existing = await _context.MeterMakes
                .FirstOrDefaultAsync(x => x.Id == entity.Id && !x.IsDeleted, cancellationToken);

            if (existing == null)
                return Result.Fail<MeterMake>($"Meter make with ID {entity.Id} was not found.");

            existing.MeterMakeName = entity.MeterMakeName;
            existing.IsActive = entity.IsActive;
            existing.UpdatedBy = entity.UpdatedBy;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Ok(existing);
        }
        catch (Exception ex)
        {
            return Result.Fail<MeterMake>(ex.Message);
        }
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var existing = await _context.MeterMakes
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);

            if (existing == null)
                return Result.Fail($"Meter make with ID {id} was not found.");

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
