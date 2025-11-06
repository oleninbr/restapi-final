using Domain.Conditioners;
using Domain.MaintenanceSchedules;
using LanguageExt;

namespace Application.Common.Interfaces.Repositories;

public interface IMaintenanceScheduleRepository
{
    Task<MaintenanceSchedule> AddAsync(MaintenanceSchedule entity, CancellationToken cancellationToken);
    Task<MaintenanceSchedule> UpdateAsync(MaintenanceSchedule entity, CancellationToken cancellationToken);
    Task<MaintenanceSchedule> DeleteAsync(MaintenanceSchedule entity, CancellationToken cancellationToken);

    Task<Option<MaintenanceSchedule>> GetByIdAsync(MaintenanceScheduleId id, CancellationToken cancellationToken);
    Task<IReadOnlyList<MaintenanceSchedule>> GetByConditionerIdAsync(ConditionerId conditionerId, CancellationToken cancellationToken);
    Task<IReadOnlyList<MaintenanceSchedule>> GetAllAsync(CancellationToken cancellationToken);
}
