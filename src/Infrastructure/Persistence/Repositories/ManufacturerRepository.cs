using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Application.Common.Settings;
using Domain.Manufacturer;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ManufacturerRepository : IManufacturerRepository, IManufacturerQueries
{
    private readonly ApplicationDbContext _context;

    public ManufacturerRepository(ApplicationDbContext context, ApplicationSettings settings)
    {
        var connectionString = settings.ConnectionStrings.DefaultConnection;

        _context = context;
    }

    public async Task<Manufacturer> AddAsync(Manufacturer entity, CancellationToken cancellationToken)
    {
        await _context.Manufacturers.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<IReadOnlyList<Manufacturer>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Manufacturers.ToListAsync(cancellationToken);
    }

    public async Task<Manufacturer> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Manufacturers.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task UpdateAsync(Manufacturer entity, CancellationToken cancellationToken)
    {
        _context.Manufacturers.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Manufacturers.FindAsync(new object[] { id }, cancellationToken);
        if (entity != null)
        {
            _context.Manufacturers.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
