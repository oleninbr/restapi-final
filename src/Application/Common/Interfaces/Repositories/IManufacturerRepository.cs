using Domain.Manufacturers;
using LanguageExt;

namespace Application.Common.Interfaces.Repositories;

public interface IManufacturerRepository
{
    Task<Manufacturer> AddAsync(Manufacturer entity, CancellationToken cancellationToken);
    Task<Manufacturer> UpdateAsync(Manufacturer entity, CancellationToken cancellationToken);
    Task<Manufacturer> DeleteAsync(Manufacturer entity, CancellationToken cancellationToken);

    Task<Option<Manufacturer>> GetByIdAsync(ManufacturerId id, CancellationToken cancellationToken);
    Task<Option<Manufacturer>> GetByNameAsync(string name, CancellationToken cancellationToken);
    Task<IReadOnlyList<Manufacturer>> GetAllAsync(CancellationToken cancellationToken);
}
