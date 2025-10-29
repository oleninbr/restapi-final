using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Application.Common.Settings;
using Domain.WorkOrder;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class WorkOrderRepository : IWorkOrderRepository, IWorkOrderQueries
{
    private readonly ApplicationDbContext _context;

    public WorkOrderRepository(ApplicationDbContext context, ApplicationSettings settings)
    {
        var connectionString = settings.ConnectionStrings.DefaultConnection;
        _context = context;
    }

    public async Task<WorkOrder> AddAsync(WorkOrder entity, CancellationToken cancellationToken)
    {
        await _context.WorkOrders.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<IReadOnlyList<WorkOrder>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.WorkOrders.ToListAsync(cancellationToken);
    }

    public async Task<WorkOrder> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.WorkOrders.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task UpdateAsync(WorkOrder entity, CancellationToken cancellationToken)
    {
        _context.WorkOrders.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.WorkOrders.FindAsync(new object[] { id }, cancellationToken);
        if (entity != null)
        {
            _context.WorkOrders.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
