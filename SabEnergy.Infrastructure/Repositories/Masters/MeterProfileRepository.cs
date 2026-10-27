using FluentResults;
using Microsoft.EntityFrameworkCore;
using SabEnergy.Application.Modules.Interfaces.Masters;
using SabEnergy.Domain.Modules.Masters;
using SabEnergy.Infrastructure.Persistence;

namespace SabEnergy.Infrastructure.Repositories.Masters;

public class MeterProfileRepository : IMeterProfileRepository
{
    private readonly ApplicationDbContext _context;

    public MeterProfileRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<MeterProfile>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var entities = await _context.MeterProfiles
                .Include(x => x.MeterMake)
                .Include(x => x.MeterModel)
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.Id)
                .ToListAsync(cancellationToken);

            return Result.Ok<IEnumerable<MeterProfile>>(entities);
        }
        catch (Exception ex)
        {
            return Result.Fail<IEnumerable<MeterProfile>>(ex.Message);
        }
    }

    public async Task<Result<MeterProfile>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await _context.MeterProfiles
                .Include(x => x.MeterMake)
                .Include(x => x.MeterModel)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);

            if (entity == null)
                return Result.Fail<MeterProfile>($"Meter profile with ID {id} was not found.");

            return Result.Ok(entity);
        }
        catch (Exception ex)
        {
            return Result.Fail<MeterProfile>(ex.Message);
        }
    }

    public async Task<Result<MeterProfile>> CreateAsync(MeterProfile entity, CancellationToken cancellationToken = default)
    {
        try
        {
            var makeExists = await _context.MeterMakes
                .AnyAsync(x => x.Id == entity.MakeId && !x.IsDeleted, cancellationToken);

            if (!makeExists)
                return Result.Fail<MeterProfile>($"Meter make with ID {entity.MakeId} was not found.");

            var modelExists = await _context.MeterModels
                .AnyAsync(x => x.Id == entity.ModelId && !x.IsDeleted, cancellationToken);

            if (!modelExists)
                return Result.Fail<MeterProfile>($"Meter model with ID {entity.ModelId} was not found.");

            await _context.MeterProfiles.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            await _context.Entry(entity).Reference(x => x.MeterMake).LoadAsync(cancellationToken);
            await _context.Entry(entity).Reference(x => x.MeterModel).LoadAsync(cancellationToken);

            return Result.Ok(entity);
        }
        catch (Exception ex)
        {
            return Result.Fail<MeterProfile>(ex.Message);
        }
    }

    public async Task<Result<MeterProfile>> UpdateAsync(MeterProfile entity, CancellationToken cancellationToken = default)
    {
        try
        {
            var existing = await _context.MeterProfiles
                .Include(x => x.MeterMake)
                .Include(x => x.MeterModel)
                .FirstOrDefaultAsync(x => x.Id == entity.Id && !x.IsDeleted, cancellationToken);

            if (existing == null)
                return Result.Fail<MeterProfile>($"Meter profile with ID {entity.Id} was not found.");

            var makeExists = await _context.MeterMakes
                .AnyAsync(x => x.Id == entity.MakeId && !x.IsDeleted, cancellationToken);

            if (!makeExists)
                return Result.Fail<MeterProfile>($"Meter make with ID {entity.MakeId} was not found.");

            var modelExists = await _context.MeterModels
                .AnyAsync(x => x.Id == entity.ModelId && !x.IsDeleted, cancellationToken);

            if (!modelExists)
                return Result.Fail<MeterProfile>($"Meter model with ID {entity.ModelId} was not found.");

            existing.ProfileVersion = entity.ProfileVersion;
            existing.MakeId = entity.MakeId;
            existing.ModelId = entity.ModelId;
            existing.AddressOffest = entity.AddressOffest;
            existing.HoldingRegister = entity.HoldingRegister;
            existing.ByteOrder = entity.ByteOrder;
            existing.IsActive = entity.IsActive;
            existing.UpdatedBy = entity.UpdatedBy;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Ok(existing);
        }
        catch (Exception ex)
        {
            return Result.Fail<MeterProfile>(ex.Message);
        }
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var existing = await _context.MeterProfiles
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);

            if (existing == null)
                return Result.Fail($"Meter profile with ID {id} was not found.");

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
