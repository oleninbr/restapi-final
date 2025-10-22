using Domain.Entities;

namespace Application.Common.Interfaces.Queries
{
    public interface IMaintenanceFrequencyQueries
    {
        Task<IReadOnlyList<MaintenanceFrequency>> GetAllAsync(CancellationToken cancellationToken);
        Task<MaintenanceFrequency> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
