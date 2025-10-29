using Domain.ConditionerStatus;

namespace Application.Common.Interfaces.Queries
{
    public interface IConditionerStatusQueries
    {
        Task<IReadOnlyList<ConditionerStatus>> GetAllAsync(CancellationToken cancellationToken);
        Task<ConditionerStatus> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
