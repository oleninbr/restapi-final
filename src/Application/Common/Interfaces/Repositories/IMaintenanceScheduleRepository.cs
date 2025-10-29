using Domain.MaintenanceSchedules;

namespace Application.Common.Interfaces.Repositories
{
    public interface IMaintenanceScheduleRepository
    {
        Task<MaintenanceSchedule> AddAsync(MaintenanceSchedule entity, CancellationToken cancellationToken);
        Task<MaintenanceSchedule> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IReadOnlyList<MaintenanceSchedule>> GetAllAsync(CancellationToken cancellationToken);
        Task UpdateAsync(MaintenanceSchedule entity, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
