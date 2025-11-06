using Domain.Conditioners;
using Domain.MaintenanceSchedules;
using LanguageExt;

namespace Application.Common.Interfaces.Queries;

public interface IMaintenanceScheduleQueries
{
    Task<IReadOnlyList<MaintenanceSchedule>> GetAllAsync(CancellationToken cancellationToken);
    Task<Option<MaintenanceSchedule>> GetByIdAsync(MaintenanceScheduleId id, CancellationToken cancellationToken);
    Task<IReadOnlyList<MaintenanceSchedule>> GetByConditionerIdAsync(ConditionerId conditionerId, CancellationToken cancellationToken);
}
