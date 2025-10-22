using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Application.Common.Settings;
using Domain.Entities;
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

    public async Task<MaintenanceFrequency> AddAsync(MaintenanceFrequency entity, CancellationToken cancellationToken)
    {
        await _context.MaintenanceFrequencies.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<IReadOnlyList<MaintenanceFrequency>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.MaintenanceFrequencies.ToListAsync(cancellationToken);
    }

    public async Task<MaintenanceFrequency> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.MaintenanceFrequencies.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task UpdateAsync(MaintenanceFrequency entity, CancellationToken cancellationToken)
    {
        _context.MaintenanceFrequencies.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.MaintenanceFrequencies.FindAsync(new object[] { id }, cancellationToken);
        if (entity != null)
        {
            _context.MaintenanceFrequencies.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
