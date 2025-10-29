using Domain.Conditioner;

namespace Application.Common.Interfaces.Queries
{
    public interface IConditionerQueries
    {
        Task<IReadOnlyList<Conditioner>> GetAllAsync(CancellationToken cancellationToken);
        Task<Conditioner> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
