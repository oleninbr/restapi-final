using Domain.ConditionerTypes;
using LanguageExt;

namespace Application.Common.Interfaces.Repositories;

public interface IConditionerTypeRepository
{
    Task<ConditionerType> AddAsync(ConditionerType entity, CancellationToken cancellationToken);
    Task<ConditionerType> UpdateAsync(ConditionerType entity, CancellationToken cancellationToken);
    Task<ConditionerType> DeleteAsync(ConditionerType entity, CancellationToken cancellationToken);

    Task<Option<ConditionerType>> GetByIdAsync(ConditionerTypeId id, CancellationToken cancellationToken);
    Task<Option<ConditionerType>> GetByNameAsync(string name, CancellationToken cancellationToken);
    Task<IReadOnlyList<ConditionerType>> GetAllAsync(CancellationToken cancellationToken);
}
