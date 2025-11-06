using Domain.WorkOrders;
using LanguageExt;

namespace Application.Common.Interfaces.Repositories;

public interface IWorkOrderRepository
{
    Task<WorkOrder> AddAsync(WorkOrder entity, CancellationToken cancellationToken);
    Task<WorkOrder> UpdateAsync(WorkOrder entity, CancellationToken cancellationToken);
    Task<WorkOrder> DeleteAsync(WorkOrder entity, CancellationToken cancellationToken);

    Task<Option<WorkOrder>> GetByIdAsync(WorkOrderId id, CancellationToken cancellationToken);
    Task<Option<WorkOrder>> GetByNumberAsync(string workOrderNumber, CancellationToken cancellationToken);
    Task<IReadOnlyList<WorkOrder>> GetAllAsync(CancellationToken cancellationToken);
}
