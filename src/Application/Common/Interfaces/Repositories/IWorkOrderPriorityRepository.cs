using Domain.WorkOrderPriorities;
using LanguageExt;

namespace Application.Common.Interfaces.Repositories;

public interface IWorkOrderPriorityRepository
{
    Task<WorkOrderPriority> AddAsync(WorkOrderPriority entity, CancellationToken cancellationToken);
    Task<WorkOrderPriority> UpdateAsync(WorkOrderPriority entity, CancellationToken cancellationToken);
    Task<WorkOrderPriority> DeleteAsync(WorkOrderPriority entity, CancellationToken cancellationToken);

    Task<Option<WorkOrderPriority>> GetByIdAsync(WorkOrderPriorityId id, CancellationToken cancellationToken);
    Task<Option<WorkOrderPriority>> GetByNameAsync(string name, CancellationToken cancellationToken);
    Task<IReadOnlyList<WorkOrderPriority>> GetAllAsync(CancellationToken cancellationToken);
}
