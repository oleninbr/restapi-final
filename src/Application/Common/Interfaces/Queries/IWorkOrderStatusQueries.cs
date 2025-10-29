using Domain.WorkOrderStatuses;

namespace Application.Common.Interfaces.Queries
{
    public interface IWorkOrderStatusQueries
    {
        Task<IReadOnlyList<WorkOrderStatus>> GetAllAsync(CancellationToken cancellationToken);
        Task<WorkOrderStatus> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
