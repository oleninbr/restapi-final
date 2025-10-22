using Domain.Entities;

namespace Application.Common.Interfaces.Queries
{
    public interface IConditionerTypeQueries
    {
        Task<IReadOnlyList<ConditionerType>> GetAllAsync(CancellationToken cancellationToken);
        Task<ConditionerType> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
