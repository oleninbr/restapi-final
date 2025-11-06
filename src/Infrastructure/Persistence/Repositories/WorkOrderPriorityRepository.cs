using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Application.Common.Settings;
using Domain.WorkOrderPriorities;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class WorkOrderPriorityRepository : IWorkOrderPriorityRepository, IWorkOrderPriorityQueries
{
    private readonly ApplicationDbContext _context;

    public WorkOrderPriorityRepository(ApplicationDbContext context, ApplicationSettings settings)
    {
        var connectionString = settings.ConnectionStrings.DefaultConnection;
        _context = context;
    }

    public async Task<Option<WorkOrderPriority>> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        var entity = await _context.WorkOrderPriorities
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == name, cancellationToken);

        return entity ?? Option<WorkOrderPriority>.None;
    }

    public async Task<WorkOrderPriority> AddAsync(WorkOrderPriority entity, CancellationToken cancellationToken)
    {
        await _context.WorkOrderPriorities.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<WorkOrderPriority> UpdateAsync(WorkOrderPriority entity, CancellationToken cancellationToken)
    {
        _context.WorkOrderPriorities.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<WorkOrderPriority> DeleteAsync(WorkOrderPriority entity, CancellationToken cancellationToken)
    {
        _context.WorkOrderPriorities.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Option<WorkOrderPriority>> GetByIdAsync(WorkOrderPriorityId id, CancellationToken cancellationToken)
    {
        var entity = await _context.WorkOrderPriorities
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);

        return entity ?? Option<WorkOrderPriority>.None;
    }

    public async Task<IReadOnlyList<WorkOrderPriority>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.WorkOrderPriorities
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
