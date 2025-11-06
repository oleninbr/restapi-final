using Domain.ConditionerStatuses;
using LanguageExt;

namespace Application.Common.Interfaces.Repositories;

public interface IConditionerStatusRepository
{
    Task<ConditionerStatus> AddAsync(ConditionerStatus entity, CancellationToken cancellationToken);
    Task<ConditionerStatus> UpdateAsync(ConditionerStatus entity, CancellationToken cancellationToken);
    Task<ConditionerStatus> DeleteAsync(ConditionerStatus entity, CancellationToken cancellationToken);

    Task<Option<ConditionerStatus>> GetByIdAsync(ConditionerStatusId id, CancellationToken cancellationToken);
    Task<Option<ConditionerStatus>> GetByNameAsync(string name, CancellationToken cancellationToken);
    Task<IReadOnlyList<ConditionerStatus>> GetAllAsync(CancellationToken cancellationToken);
}
