using Domain.Entities;

namespace Application.Common.Interfaces.Repositories
{
    public interface IWorkOrderPriorityRepository
    {
        Task<WorkOrderPriority> AddAsync(WorkOrderPriority entity, CancellationToken cancellationToken);
        Task<WorkOrderPriority> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IReadOnlyList<WorkOrderPriority>> GetAllAsync(CancellationToken cancellationToken);
        Task UpdateAsync(WorkOrderPriority entity, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
