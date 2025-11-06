using Domain.Conditioners;
using LanguageExt;

namespace Application.Common.Interfaces.Repositories;

public interface IConditionerRepository
{
    Task<Conditioner> AddAsync(Conditioner entity, CancellationToken cancellationToken);
    Task<Conditioner> UpdateAsync(Conditioner entity, CancellationToken cancellationToken);
    Task<Conditioner> DeleteAsync(Conditioner entity, CancellationToken cancellationToken);

    Task<Option<Conditioner>> GetByIdAsync(ConditionerId id, CancellationToken cancellationToken);
    Task<Option<Conditioner>> GetBySerialNumberAsync(string serialNumber, CancellationToken cancellationToken);
    Task<IReadOnlyList<Conditioner>> GetAllAsync(CancellationToken cancellationToken);
}
