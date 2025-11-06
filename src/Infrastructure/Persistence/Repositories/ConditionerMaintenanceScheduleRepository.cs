using Application.Common.Interfaces.Repositories;
using Domain.Conditioners;
using Domain.MaintenanceSchedules;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ConditionerMaintenanceScheduleRepository(ApplicationDbContext context)
    : IConditionerMaintenanceScheduleRepository
{
    public async Task<IReadOnlyList<ConditionerMaintenanceSchedule>> AddRangeAsync(
        IReadOnlyList<ConditionerMaintenanceSchedule> entities,
        CancellationToken cancellationToken)
    {
        await context.ConditionerMaintenanceSchedules.AddRangeAsync(entities, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entities.ToList();
    }

    public async Task<IReadOnlyList<ConditionerMaintenanceSchedule>> RemoveRangeAsync(
        IReadOnlyList<ConditionerMaintenanceSchedule> entities,
        CancellationToken cancellationToken)
    {
        context.ConditionerMaintenanceSchedules.RemoveRange(entities);
        await context.SaveChangesAsync(cancellationToken);
        return entities.ToList();
    }

    public async Task<IReadOnlyList<ConditionerMaintenanceSchedule>> GetByConditionerIdAsync(
        ConditionerId conditionerId,
        CancellationToken cancellationToken)
    {
        return await context.ConditionerMaintenanceSchedules
            .Where(x => x.ConditionerId == conditionerId)
            .Include(x => x.MaintenanceSchedule)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ConditionerMaintenanceSchedule>> GetByMaintenanceScheduleIdAsync(
        MaintenanceScheduleId scheduleId,
        CancellationToken cancellationToken)
    {
        return await context.ConditionerMaintenanceSchedules
            .Where(x => x.MaintenanceScheduleId == scheduleId)
            .Include(x => x.Conditioner)
            .ToListAsync(cancellationToken);
    }
}
