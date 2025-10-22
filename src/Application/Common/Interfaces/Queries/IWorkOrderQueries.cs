using Domain.Entities;

namespace Application.Common.Interfaces.Queries
{
    public interface IWorkOrderQueries
    {
        Task<IReadOnlyList<WorkOrder>> GetAllAsync(CancellationToken cancellationToken);
        Task<WorkOrder> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
