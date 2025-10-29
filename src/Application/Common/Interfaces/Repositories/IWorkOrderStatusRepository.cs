using Domain.WorkOrderStatuses;

namespace Application.Common.Interfaces.Repositories
{
    public interface IWorkOrderStatusRepository
    {
        Task<WorkOrderStatus> AddAsync(WorkOrderStatus entity, CancellationToken cancellationToken);
        Task<WorkOrderStatus> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IReadOnlyList<WorkOrderStatus>> GetAllAsync(CancellationToken cancellationToken);
        Task UpdateAsync(WorkOrderStatus entity, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
