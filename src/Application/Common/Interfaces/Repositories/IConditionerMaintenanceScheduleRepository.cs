using Domain.Conditioners;
using Domain.MaintenanceSchedules;
using LanguageExt;

namespace Application.Common.Interfaces.Repositories;

public interface IConditionerMaintenanceScheduleRepository
{
    Task<IReadOnlyList<ConditionerMaintenanceSchedule>> AddRangeAsync(
        IReadOnlyList<ConditionerMaintenanceSchedule> entities,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<ConditionerMaintenanceSchedule>> RemoveRangeAsync(
        IReadOnlyList<ConditionerMaintenanceSchedule> entities,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<ConditionerMaintenanceSchedule>> GetByConditionerIdAsync(
        ConditionerId conditionerId,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<ConditionerMaintenanceSchedule>> GetByMaintenanceScheduleIdAsync(
        MaintenanceScheduleId scheduleId,
        CancellationToken cancellationToken);
}
