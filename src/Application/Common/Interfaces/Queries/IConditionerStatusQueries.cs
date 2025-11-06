using Domain.ConditionerStatuses;
using LanguageExt;

namespace Application.Common.Interfaces.Queries;

public interface IConditionerStatusQueries
{
    Task<IReadOnlyList<ConditionerStatus>> GetAllAsync(CancellationToken cancellationToken);
    Task<Option<ConditionerStatus>> GetByIdAsync(ConditionerStatusId id, CancellationToken cancellationToken);
}
