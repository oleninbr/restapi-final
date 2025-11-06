using Domain.MaintenanceFrequencies;
using LanguageExt;

namespace Application.Common.Interfaces.Queries;

public interface IMaintenanceFrequencyQueries
{
    Task<IReadOnlyList<MaintenanceFrequency>> GetAllAsync(CancellationToken cancellationToken);
    Task<Option<MaintenanceFrequency>> GetByIdAsync(MaintenanceFrequencyId id, CancellationToken cancellationToken);
}
