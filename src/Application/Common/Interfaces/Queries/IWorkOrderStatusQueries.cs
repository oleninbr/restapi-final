using Domain.WorkOrderStatuses;
using LanguageExt;

namespace Application.Common.Interfaces.Queries;

public interface IWorkOrderStatusQueries
{
    Task<IReadOnlyList<WorkOrderStatus>> GetAllAsync(CancellationToken cancellationToken);
    Task<Option<WorkOrderStatus>> GetByIdAsync(WorkOrderStatusId id, CancellationToken cancellationToken);
}
