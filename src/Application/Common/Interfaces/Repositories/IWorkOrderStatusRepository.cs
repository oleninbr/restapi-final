using Domain.WorkOrderStatuses;
using LanguageExt;

namespace Application.Common.Interfaces.Repositories;

public interface IWorkOrderStatusRepository
{
    Task<WorkOrderStatus> AddAsync(WorkOrderStatus entity, CancellationToken cancellationToken);
    Task<WorkOrderStatus> UpdateAsync(WorkOrderStatus entity, CancellationToken cancellationToken);
    Task<WorkOrderStatus> DeleteAsync(WorkOrderStatus entity, CancellationToken cancellationToken);

    Task<Option<WorkOrderStatus>> GetByIdAsync(WorkOrderStatusId id, CancellationToken cancellationToken);
    Task<Option<WorkOrderStatus>> GetByNameAsync(string name, CancellationToken cancellationToken);
    Task<IReadOnlyList<WorkOrderStatus>> GetAllAsync(CancellationToken cancellationToken);
}
