using FluentResults;
using Microsoft.EntityFrameworkCore;
using SabEnergy.Application.Modules.Interfaces.Masters;
using SabEnergy.Domain.Modules.Masters;
using SabEnergy.Infrastructure.Persistence;

namespace SabEnergy.Infrastructure.Repositories.Masters;

public class MeterRepository : IMeterRepository
{
    private readonly ApplicationDbContext _context;

    public MeterRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<Meter>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var entities = await _context.Meters
                .Include(x => x.MeterMake)
                .Include(x => x.MeterModel)
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.Id)
                .ToListAsync(cancellationToken);

            return Result.Ok<IEnumerable<Meter>>(entities);
        }
        catch (Exception ex)
        {
            return Result.Fail<IEnumerable<Meter>>(ex.Message);
        }
    }

    public async Task<Result<Meter>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await _context.Meters
                .Include(x => x.MeterMake)
                .Include(x => x.MeterModel)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);

            if (entity == null)
                return Result.Fail<Meter>($"Meter with ID {id} was not found.");

            return Result.Ok(entity);
        }
        catch (Exception ex)
        {
            return Result.Fail<Meter>(ex.Message);
        }
    }

    public async Task<Result<Meter>> CreateAsync(Meter entity, CancellationToken cancellationToken = default)
    {
        try
        {
            var makeExists = await _context.MeterMakes
                .AnyAsync(x => x.Id == entity.MeterMakeId && !x.IsDeleted, cancellationToken);

            if (!makeExists)
                return Result.Fail<Meter>($"Meter make with ID {entity.MeterMakeId} was not found.");

            if (entity.MeterModelId.HasValue)
            {
                var modelExists = await _context.MeterModels
                    .AnyAsync(x => x.Id == entity.MeterModelId.Value && !x.IsDeleted, cancellationToken);

                if (!modelExists)
                    return Result.Fail<Meter>($"Meter model with ID {entity.MeterModelId.Value} was not found.");
            }

            await _context.Meters.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            await _context.Entry(entity).Reference(x => x.MeterMake).LoadAsync(cancellationToken);
            if (entity.MeterModelId.HasValue)
            {
                await _context.Entry(entity).Reference(x => x.MeterModel).LoadAsync(cancellationToken);
            }

            return Result.Ok(entity);
        }
        catch (Exception ex)
        {
            return Result.Fail<Meter>(ex.Message);
        }
    }

    public async Task<Result<Meter>> UpdateAsync(Meter entity, CancellationToken cancellationToken = default)
    {
        try
        {
            var existing = await _context.Meters
                .Include(x => x.MeterMake)
                .Include(x => x.MeterModel)
                .FirstOrDefaultAsync(x => x.Id == entity.Id && !x.IsDeleted, cancellationToken);

            if (existing == null)
                return Result.Fail<Meter>($"Meter with ID {entity.Id} was not found.");

            var makeExists = await _context.MeterMakes
                .AnyAsync(x => x.Id == entity.MeterMakeId && !x.IsDeleted, cancellationToken);

            if (!makeExists)
                return Result.Fail<Meter>($"Meter make with ID {entity.MeterMakeId} was not found.");

            if (entity.MeterModelId.HasValue)
            {
                var modelExists = await _context.MeterModels
                    .AnyAsync(x => x.Id == entity.MeterModelId.Value && !x.IsDeleted, cancellationToken);

                if (!modelExists)
                    return Result.Fail<Meter>($"Meter model with ID {entity.MeterModelId.Value} was not found.");
            }

            existing.SerialNo = entity.SerialNo;
            existing.CustomName = entity.CustomName;
            existing.ChannelId = entity.ChannelId;
            existing.SlaveId = entity.SlaveId;
            existing.DeviceProfileId = entity.DeviceProfileId;
            existing.MeterMakeId = entity.MeterMakeId;
            existing.MeterModelId = entity.MeterModelId;
            existing.Status = entity.Status;
            existing.BaudRate = entity.BaudRate;
            existing.ParityBit = entity.ParityBit;
            existing.StopBit = entity.StopBit;
            existing.DataBit = entity.DataBit;
            existing.IsActive = entity.IsActive;
            existing.UpdatedBy = entity.UpdatedBy;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Ok(existing);
        }
        catch (Exception ex)
        {
            return Result.Fail<Meter>(ex.Message);
        }
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var existing = await _context.Meters
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);

            if (existing == null)
                return Result.Fail($"Meter with ID {id} was not found.");

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
