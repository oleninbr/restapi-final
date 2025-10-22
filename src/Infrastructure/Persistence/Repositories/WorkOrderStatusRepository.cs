using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Application.Common.Settings;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Persistence.Repositories;

public class WorkOrderStatusRepository : IWorkOrderStatusRepository, IWorkOrderStatusQueries
{
    private readonly ApplicationDbContext _context;

    public WorkOrderStatusRepository(ApplicationDbContext context, ApplicationSettings settings)
    {
        var connectionString = settings.ConnectionStrings.DefaultConnection;
        _context = context;
    }

    public async Task<WorkOrderStatus> AddAsync(WorkOrderStatus entity, CancellationToken cancellationToken)
    {
        await _context.WorkOrderStatuses.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<IReadOnlyList<WorkOrderStatus>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.WorkOrderStatuses.ToListAsync(cancellationToken);
    }

    public async Task<WorkOrderStatus> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.WorkOrderStatuses.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task UpdateAsync(WorkOrderStatus entity, CancellationToken cancellationToken)
    {
        _context.WorkOrderStatuses.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.WorkOrderStatuses.FindAsync(new object[] { id }, cancellationToken);
        if (entity != null)
        {
            _context.WorkOrderStatuses.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
