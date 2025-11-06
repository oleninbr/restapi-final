using Domain.Manufacturers;
using LanguageExt;

namespace Application.Common.Interfaces.Queries;

public interface IManufacturerQueries
{
    Task<IReadOnlyList<Manufacturer>> GetAllAsync(CancellationToken cancellationToken);
    Task<Option<Manufacturer>> GetByIdAsync(ManufacturerId id, CancellationToken cancellationToken);
}
