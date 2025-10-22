using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Application.Common.Settings;
using Domain.Entities;
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

    public async Task<ConditionerStatus> AddAsync(ConditionerStatus entity, CancellationToken cancellationToken)
    {
        await _context.ConditionerStatuses.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<IReadOnlyList<ConditionerStatus>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.ConditionerStatuses.ToListAsync(cancellationToken);
    }

    public async Task<ConditionerStatus> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.ConditionerStatuses.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task UpdateAsync(ConditionerStatus entity, CancellationToken cancellationToken)
    {
        _context.ConditionerStatuses.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.ConditionerStatuses.FindAsync(new object[] { id }, cancellationToken);
        if (entity != null)
        {
            _context.ConditionerStatuses.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
