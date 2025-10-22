using Domain.Entities;

namespace Application.Common.Interfaces.Repositories
{
    public interface IMaintenanceFrequencyRepository
    {
        Task<MaintenanceFrequency> AddAsync(MaintenanceFrequency entity, CancellationToken cancellationToken);
        Task<MaintenanceFrequency> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IReadOnlyList<MaintenanceFrequency>> GetAllAsync(CancellationToken cancellationToken);
        Task UpdateAsync(MaintenanceFrequency entity, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
