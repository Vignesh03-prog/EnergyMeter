using FluentResults;
using Microsoft.EntityFrameworkCore;
using SabEnergy.Application.Modules.Interfaces.Masters;
using SabEnergy.Domain.Modules.Masters;
using SabEnergy.Infrastructure.Persistence;

namespace SabEnergy.Infrastructure.Repositories.Masters;

public class ParameterMasterRepository : IParameterMasterRepository
{
    private readonly ApplicationDbContext _context;

    public ParameterMasterRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IEnumerable<ParameterMaster>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var entities = await _context.ParameterMasters
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.Id)
                .ToListAsync(cancellationToken);

            return Result.Ok<IEnumerable<ParameterMaster>>(entities);
        }
        catch (Exception ex)
        {
            return Result.Fail<IEnumerable<ParameterMaster>>(ex.Message);
        }
    }

    public async Task<Result<ParameterMaster>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await _context.ParameterMasters
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);

            if (entity == null)
                return Result.Fail<ParameterMaster>($"Parameter master with ID {id} was not found.");

            return Result.Ok(entity);
        }
        catch (Exception ex)
        {
            return Result.Fail<ParameterMaster>(ex.Message);
        }
    }

    public async Task<Result<ParameterMaster>> CreateAsync(ParameterMaster entity, CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.ParameterMasters.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Ok(entity);
        }
        catch (Exception ex)
        {
            return Result.Fail<ParameterMaster>(ex.Message);
        }
    }

    public async Task<Result<ParameterMaster>> UpdateAsync(ParameterMaster entity, CancellationToken cancellationToken = default)
    {
        try
        {
            var existing = await _context.ParameterMasters
                .FirstOrDefaultAsync(x => x.Id == entity.Id && !x.IsDeleted, cancellationToken);

            if (existing == null)
                return Result.Fail<ParameterMaster>($"Parameter master with ID {entity.Id} was not found.");

            existing.Key = entity.Key;
            existing.DisplayName = entity.DisplayName;
            existing.CategoryId = entity.CategoryId;
            existing.Unit = entity.Unit;
            existing.IsActive = entity.IsActive;
            existing.UpdatedBy = entity.UpdatedBy;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Ok(existing);
        }
        catch (Exception ex)
        {
            return Result.Fail<ParameterMaster>(ex.Message);
        }
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var existing = await _context.ParameterMasters
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);

            if (existing == null)
                return Result.Fail($"Parameter master with ID {id} was not found.");

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
