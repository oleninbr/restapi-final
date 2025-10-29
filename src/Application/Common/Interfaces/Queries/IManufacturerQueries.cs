using Domain.Manufacturer;

namespace Application.Common.Interfaces.Queries
{
    public interface IManufacturerQueries
    {
        Task<IReadOnlyList<Manufacturer>> GetAllAsync(CancellationToken cancellationToken);
        Task<Manufacturer> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
