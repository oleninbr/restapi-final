using Domain.MaintenanceFrequencies;
using LanguageExt;

namespace Application.Common.Interfaces.Repositories;

public interface IMaintenanceFrequencyRepository
{
    Task<MaintenanceFrequency> AddAsync(MaintenanceFrequency entity, CancellationToken cancellationToken);
    Task<MaintenanceFrequency> UpdateAsync(MaintenanceFrequency entity, CancellationToken cancellationToken);
    Task<MaintenanceFrequency> DeleteAsync(MaintenanceFrequency entity, CancellationToken cancellationToken);

    Task<Option<MaintenanceFrequency>> GetByIdAsync(MaintenanceFrequencyId id, CancellationToken cancellationToken);
    Task<Option<MaintenanceFrequency>> GetByNameAsync(string name, CancellationToken cancellationToken);
    Task<IReadOnlyList<MaintenanceFrequency>> GetAllAsync(CancellationToken cancellationToken);
}
