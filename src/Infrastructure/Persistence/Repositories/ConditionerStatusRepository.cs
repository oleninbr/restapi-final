using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Application.Common.Settings;
using Domain.ConditionerStatuses;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ConditionerStatusRepository : IConditionerStatusRepository, IConditionerStatusQueries
{
    private readonly ApplicationDbContext _context;

    public ConditionerStatusRepository(ApplicationDbContext context, ApplicationSettings settings)
    {
        var connectionString = settings.ConnectionStrings.DefaultConnection;
        _context = context;
    }

    public async Task<Option<ConditionerStatus>> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        var entity = await _context.ConditionerStatuses
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == name, cancellationToken);

        return entity ?? Option<ConditionerStatus>.None;
    }

    public async Task<ConditionerStatus> AddAsync(ConditionerStatus entity, CancellationToken cancellationToken)
    {
        await _context.ConditionerStatuses.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<ConditionerStatus> UpdateAsync(ConditionerStatus entity, CancellationToken cancellationToken)
    {
        _context.ConditionerStatuses.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<ConditionerStatus> DeleteAsync(ConditionerStatus entity, CancellationToken cancellationToken)
    {
        _context.ConditionerStatuses.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Option<ConditionerStatus>> GetByIdAsync(ConditionerStatusId id, CancellationToken cancellationToken)
    {
        var entity = await _context.ConditionerStatuses
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);

        return entity ?? Option<ConditionerStatus>.None;
    }

    public async Task<IReadOnlyList<ConditionerStatus>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.ConditionerStatuses
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
