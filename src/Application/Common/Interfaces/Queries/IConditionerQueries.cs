using Domain.Conditioners;
using LanguageExt;

namespace Application.Common.Interfaces.Queries;

public interface IConditionerQueries
{
    Task<IReadOnlyList<Conditioner>> GetAllAsync(CancellationToken cancellationToken);
    Task<Option<Conditioner>> GetByIdAsync(ConditionerId id, CancellationToken cancellationToken);
}
