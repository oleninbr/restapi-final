using Domain.ConditionerType;

namespace Application.Common.Interfaces.Repositories
{
    public interface IConditionerTypeRepository
    {
        Task<ConditionerType> AddAsync(ConditionerType entity, CancellationToken cancellationToken);
        Task<ConditionerType> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IReadOnlyList<ConditionerType>> GetAllAsync(CancellationToken cancellationToken);
        Task UpdateAsync(ConditionerType entity, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
