using Domain.WorkOrderPriorities;
using LanguageExt;

namespace Application.Common.Interfaces.Queries;

public interface IWorkOrderPriorityQueries
{
    Task<IReadOnlyList<WorkOrderPriority>> GetAllAsync(CancellationToken cancellationToken);
    Task<Option<WorkOrderPriority>> GetByIdAsync(WorkOrderPriorityId id, CancellationToken cancellationToken);
}
