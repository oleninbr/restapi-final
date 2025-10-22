using Domain.Entities;

namespace Application.Common.Interfaces.Repositories
{
    public interface IConditionerRepository
    {
        Task<Conditioner> AddAsync(Conditioner entity, CancellationToken cancellationToken);
        Task<Conditioner> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IReadOnlyList<Conditioner>> GetAllAsync(CancellationToken cancellationToken);
        Task UpdateAsync(Conditioner entity, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
