using Domain.ConditionerTypes;
using LanguageExt;

namespace Application.Common.Interfaces.Queries;

public interface IConditionerTypeQueries
{
    Task<IReadOnlyList<ConditionerType>> GetAllAsync(CancellationToken cancellationToken);
    Task<Option<ConditionerType>> GetByIdAsync(ConditionerTypeId id, CancellationToken cancellationToken);
}
