using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Application.Common.Settings;
using Domain.MaintenanceFrequencies;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class MaintenanceFrequencyRepository : IMaintenanceFrequencyRepository, IMaintenanceFrequencyQueries
{
    private readonly ApplicationDbContext _context;

    public MaintenanceFrequencyRepository(ApplicationDbContext context, ApplicationSettings settings)
    {
        var connectionString = settings.ConnectionStrings.DefaultConnection;
        _context = context;
    }
    public async Task<Option<MaintenanceFrequency>> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        var entity = await _context.MaintenanceFrequencies
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == name, cancellationToken);

        return entity ?? Option<MaintenanceFrequency>.None;
    }

    public async Task<MaintenanceFrequency> AddAsync(MaintenanceFrequency entity, CancellationToken cancellationToken)
    {
        await _context.MaintenanceFrequencies.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<MaintenanceFrequency> UpdateAsync(MaintenanceFrequency entity, CancellationToken cancellationToken)
    {
        _context.MaintenanceFrequencies.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<MaintenanceFrequency> DeleteAsync(MaintenanceFrequency entity, CancellationToken cancellationToken)
    {
        _context.MaintenanceFrequencies.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Option<MaintenanceFrequency>> GetByIdAsync(MaintenanceFrequencyId id, CancellationToken cancellationToken)
    {
        var entity = await _context.MaintenanceFrequencies
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);

        return entity ?? Option<MaintenanceFrequency>.None;
    }

    public async Task<IReadOnlyList<MaintenanceFrequency>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.MaintenanceFrequencies
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
