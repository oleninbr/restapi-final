using Domain.WorkOrders;
using LanguageExt;

namespace Application.Common.Interfaces.Queries;

public interface IWorkOrderQueries
{
    Task<IReadOnlyList<WorkOrder>> GetAllAsync(CancellationToken cancellationToken);
    Task<Option<WorkOrder>> GetByIdAsync(WorkOrderId id, CancellationToken cancellationToken);
    Task<Option<WorkOrder>> GetByNumberAsync(string workOrderNumber, CancellationToken cancellationToken);
}
