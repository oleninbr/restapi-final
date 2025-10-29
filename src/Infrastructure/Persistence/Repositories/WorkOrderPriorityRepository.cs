using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Application.Common.Settings;
using Domain.WorkOrderPriorities;
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

    public async Task<WorkOrderPriority> AddAsync(WorkOrderPriority entity, CancellationToken cancellationToken)
    {
        await _context.WorkOrderPriorities.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<IReadOnlyList<WorkOrderPriority>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.WorkOrderPriorities.ToListAsync(cancellationToken);
    }

    public async Task<WorkOrderPriority> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.WorkOrderPriorities.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task UpdateAsync(WorkOrderPriority entity, CancellationToken cancellationToken)
    {
        _context.WorkOrderPriorities.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.WorkOrderPriorities.FindAsync(new object[] { id }, cancellationToken);
        if (entity != null)
        {
            _context.WorkOrderPriorities.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
