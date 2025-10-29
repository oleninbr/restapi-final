using Domain.Manufacturer;


namespace Application.Common.Interfaces.Repositories
{
    public interface IManufacturerRepository
    {
        Task<Manufacturer> AddAsync(Manufacturer entity, CancellationToken cancellationToken);
        Task<Manufacturer> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IReadOnlyList<Manufacturer>> GetAllAsync(CancellationToken cancellationToken);
        Task UpdateAsync(Manufacturer entity, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
