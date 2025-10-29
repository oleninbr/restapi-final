using Domain.MaintenanceSchedules;

namespace Application.Common.Interfaces.Queries
{
    public interface IMaintenanceScheduleQueries
    {
        Task<IReadOnlyList<MaintenanceSchedule>> GetAllAsync(CancellationToken cancellationToken);
        Task<MaintenanceSchedule> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
