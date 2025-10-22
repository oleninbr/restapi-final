using Domain.Entities;

namespace Application.Common.Interfaces.Queries
{
    public interface IWorkOrderPriorityQueries
    {
        Task<IReadOnlyList<WorkOrderPriority>> GetAllAsync(CancellationToken cancellationToken);
        Task<WorkOrderPriority> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
