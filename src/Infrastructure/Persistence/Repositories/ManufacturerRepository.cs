using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Application.Common.Settings;
using Domain.Manufacturers;
using LanguageExt;
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

    public async Task<Option<Manufacturer>> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        var entity = await _context.Manufacturers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == name, cancellationToken);

        return entity ?? Option<Manufacturer>.None;
    }

    public async Task<Manufacturer> AddAsync(Manufacturer entity, CancellationToken cancellationToken)
    {
        await _context.Manufacturers.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Manufacturer> UpdateAsync(Manufacturer entity, CancellationToken cancellationToken)
    {
        _context.Manufacturers.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Manufacturer> DeleteAsync(Manufacturer entity, CancellationToken cancellationToken)
    {
        _context.Manufacturers.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Option<Manufacturer>> GetByIdAsync(ManufacturerId id, CancellationToken cancellationToken)
    {
        var entity = await _context.Manufacturers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);

        return entity ?? Option<Manufacturer>.None;
    }

    public async Task<IReadOnlyList<Manufacturer>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Manufacturers
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
