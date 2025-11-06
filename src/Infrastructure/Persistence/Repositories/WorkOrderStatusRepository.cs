using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Application.Common.Settings;
using Domain.WorkOrderStatuses;
using LanguageExt;
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

    public async Task<Option<WorkOrderStatus>> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        var entity = await _context.WorkOrderStatuses
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == name, cancellationToken);

        return entity ?? Option<WorkOrderStatus>.None;
    }

    public async Task<WorkOrderStatus> UpdateAsync(WorkOrderStatus entity, CancellationToken cancellationToken)
    {
        _context.WorkOrderStatuses.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<WorkOrderStatus> DeleteAsync(WorkOrderStatus entity, CancellationToken cancellationToken)
    {
        _context.WorkOrderStatuses.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Option<WorkOrderStatus>> GetByIdAsync(WorkOrderStatusId id, CancellationToken cancellationToken)
    {
        var entity = await _context.WorkOrderStatuses
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);

        return entity ?? Option<WorkOrderStatus>.None;
    }

    public async Task<IReadOnlyList<WorkOrderStatus>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.WorkOrderStatuses
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
