using FluentResults;
using Microsoft.EntityFrameworkCore;
using SabEnergy.Application.Modules.Interfaces.Masters;
using SabEnergy.Domain.Modules.Masters;
using SabEnergy.Infrastructure.Persistence;

namespace SabEnergy.Infrastructure.Repositories.Masters;

public class MeterParameterRepository : IMeterParameterRepository
{
    private readonly ApplicationDbContext _context;

    public MeterParameterRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<MeterParameter>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var entities = await _context.MeterParameters
                .Include(x => x.MeterProfile)
                .Include(x => x.ParameterMaster)
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.Id)
                .ToListAsync(cancellationToken);

            return Result.Ok<IEnumerable<MeterParameter>>(entities);
        }
        catch (Exception ex)
        {
            return Result.Fail<IEnumerable<MeterParameter>>(ex.Message);
        }
    }

    public async Task<Result<MeterParameter>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await _context.MeterParameters
                .Include(x => x.MeterProfile)
                .Include(x => x.ParameterMaster)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);

            if (entity == null)
                return Result.Fail<MeterParameter>($"Meter parameter with ID {id} was not found.");

            return Result.Ok(entity);
        }
        catch (Exception ex)
        {
            return Result.Fail<MeterParameter>(ex.Message);
        }
    }

    public async Task<Result<MeterParameter>> CreateAsync(MeterParameter entity, CancellationToken cancellationToken = default)
    {
        try
        {
            var profileExists = await _context.MeterProfiles
                .AnyAsync(x => x.Id == entity.MeterProfileId && !x.IsDeleted, cancellationToken);

            if (!profileExists)
                return Result.Fail<MeterParameter>($"Meter profile with ID {entity.MeterProfileId} was not found.");

            var masterExists = await _context.ParameterMasters
                .AnyAsync(x => x.Id == entity.ParameterMasterId && !x.IsDeleted, cancellationToken);

            if (!masterExists)
                return Result.Fail<MeterParameter>($"Parameter master with ID {entity.ParameterMasterId} was not found.");

            await _context.MeterParameters.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            await _context.Entry(entity).Reference(x => x.MeterProfile).LoadAsync(cancellationToken);
            await _context.Entry(entity).Reference(x => x.ParameterMaster).LoadAsync(cancellationToken);

            return Result.Ok(entity);
        }
        catch (Exception ex)
        {
            return Result.Fail<MeterParameter>(ex.Message);
        }
    }

    public async Task<Result<MeterParameter>> UpdateAsync(MeterParameter entity, CancellationToken cancellationToken = default)
    {
        try
        {
            var existing = await _context.MeterParameters
                .Include(x => x.MeterProfile)
                .Include(x => x.ParameterMaster)
                .FirstOrDefaultAsync(x => x.Id == entity.Id && !x.IsDeleted, cancellationToken);

            if (existing == null)
                return Result.Fail<MeterParameter>($"Meter parameter with ID {entity.Id} was not found.");

            var profileExists = await _context.MeterProfiles
                .AnyAsync(x => x.Id == entity.MeterProfileId && !x.IsDeleted, cancellationToken);

            if (!profileExists)
                return Result.Fail<MeterParameter>($"Meter profile with ID {entity.MeterProfileId} was not found.");

            var masterExists = await _context.ParameterMasters
                .AnyAsync(x => x.Id == entity.ParameterMasterId && !x.IsDeleted, cancellationToken);

            if (!masterExists)
                return Result.Fail<MeterParameter>($"Parameter master with ID {entity.ParameterMasterId} was not found.");

            existing.MeterProfileId = entity.MeterProfileId;
            existing.ParameterMasterId = entity.ParameterMasterId;
            existing.Address = entity.Address;
            existing.DataType = entity.DataType;
            existing.Multiplier = entity.Multiplier;
            existing.IsActive = entity.IsActive;
            existing.UpdatedBy = entity.UpdatedBy;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Ok(existing);
        }
        catch (Exception ex)
        {
            return Result.Fail<MeterParameter>(ex.Message);
        }
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var existing = await _context.MeterParameters
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);

            if (existing == null)
                return Result.Fail($"Meter parameter with ID {id} was not found.");

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
