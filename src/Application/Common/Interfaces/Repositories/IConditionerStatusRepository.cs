using Domain.ConditionerStatus;

namespace Application.Common.Interfaces.Repositories
{
    public interface IConditionerStatusRepository
    {
        Task<ConditionerStatus> AddAsync(ConditionerStatus entity, CancellationToken cancellationToken);
        Task<ConditionerStatus> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IReadOnlyList<ConditionerStatus>> GetAllAsync(CancellationToken cancellationToken);
        Task UpdateAsync(ConditionerStatus entity, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
