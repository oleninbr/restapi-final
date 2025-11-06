using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Application.Common.Settings;
using Domain.WorkOrders;
using LanguageExt;
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

    public async Task<Option<WorkOrder>> GetByNumberAsync(string workOrderNumber, CancellationToken cancellationToken)
    {
        var entity = await _context.WorkOrders
            .Include(x => x.Conditioner)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.WorkOrderNumber == workOrderNumber, cancellationToken);

        return entity ?? Option<WorkOrder>.None;
    }

    public async Task<WorkOrder> AddAsync(WorkOrder entity, CancellationToken cancellationToken)
    {
        await _context.WorkOrders.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<WorkOrder> UpdateAsync(WorkOrder entity, CancellationToken cancellationToken)
    {
        _context.WorkOrders.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<WorkOrder> DeleteAsync(WorkOrder entity, CancellationToken cancellationToken)
    {
        _context.WorkOrders.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Option<WorkOrder>> GetByIdAsync(WorkOrderId id, CancellationToken cancellationToken)
    {
        var entity = await _context.WorkOrders
            .Include(x => x.Conditioner)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);

        return entity ?? Option<WorkOrder>.None;
    }

    public async Task<IReadOnlyList<WorkOrder>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.WorkOrders
            .Include(x => x.Conditioner)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
