using Domain.Entities;

namespace Application.Common.Interfaces.Queries
{
    public interface IConditionerQueries
    {
        Task<IReadOnlyList<Conditioner>> GetAllAsync(CancellationToken cancellationToken);
        Task<Conditioner> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
