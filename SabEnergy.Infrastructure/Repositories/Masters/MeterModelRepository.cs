using FluentResults;
using Microsoft.EntityFrameworkCore;
using SabEnergy.Application.Modules.Interfaces.Masters;
using SabEnergy.Domain.Modules.Masters.Model;
using SabEnergy.Infrastructure.Persistence;

namespace SabEnergy.Infrastructure.Repositories.Masters;

public class MeterModelRepository : IMeterModelRepository
{
    private readonly ApplicationDbContext _context;

    public MeterModelRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<MeterModel>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var entities = await _context.MeterModels
                .Include(x => x.MeterMake)
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.Id)
                .ToListAsync(cancellationToken);

            return Result.Ok<IEnumerable<MeterModel>>(entities);
        }
        catch (Exception ex)
        {
            return Result.Fail<IEnumerable<MeterModel>>(ex.Message);
        }
    }

    public async Task<Result<MeterModel>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await _context.MeterModels
                .Include(x => x.MeterMake)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);

            if (entity == null)
                return Result.Fail<MeterModel>($"Meter model with ID {id} was not found.");

            return Result.Ok(entity);
        }
        catch (Exception ex)
        {
            return Result.Fail<MeterModel>(ex.Message);
        }
    }

    public async Task<Result<MeterModel>> CreateAsync(MeterModel entity, CancellationToken cancellationToken = default)
    {
        try
        {
            var makeExists = await _context.MeterMakes
                .AnyAsync(x => x.Id == entity.MeterMakeId && !x.IsDeleted, cancellationToken);

            if (!makeExists)
                return Result.Fail<MeterModel>($"Meter make with ID {entity.MeterMakeId} was not found.");

            await _context.MeterModels.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            await _context.Entry(entity).Reference(x => x.MeterMake).LoadAsync(cancellationToken);

            return Result.Ok(entity);
        }
        catch (Exception ex)
        {
            return Result.Fail<MeterModel>(ex.Message);
        }
    }

    public async Task<Result<MeterModel>> UpdateAsync(MeterModel entity, CancellationToken cancellationToken = default)
    {
        try
        {
            var existing = await _context.MeterModels
                .Include(x => x.MeterMake)
                .FirstOrDefaultAsync(x => x.Id == entity.Id && !x.IsDeleted, cancellationToken);

            if (existing == null)
                return Result.Fail<MeterModel>($"Meter model with ID {entity.Id} was not found.");

            var makeExists = await _context.MeterMakes
                .AnyAsync(x => x.Id == entity.MeterMakeId && !x.IsDeleted, cancellationToken);

            if (!makeExists)
                return Result.Fail<MeterModel>($"Meter make with ID {entity.MeterMakeId} was not found.");

            existing.MeterModelName = entity.MeterModelName;
            existing.MeterMakeId = entity.MeterMakeId;
            existing.IsActive = entity.IsActive;
            existing.UpdatedBy = entity.UpdatedBy;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Ok(existing);
        }
        catch (Exception ex)
        {
            return Result.Fail<MeterModel>(ex.Message);
        }
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var existing = await _context.MeterModels
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);

            if (existing == null)
                return Result.Fail($"Meter model with ID {id} was not found.");

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
