using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Application.Common.Settings;
using Domain.MaintenanceSchedules;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class MaintenanceScheduleRepository : IMaintenanceScheduleRepository, IMaintenanceScheduleQueries
{
    private readonly ApplicationDbContext _context;

    public MaintenanceScheduleRepository(ApplicationDbContext context, ApplicationSettings settings)
    {
        var connectionString = settings.ConnectionStrings.DefaultConnection;
        _context = context;
    }

    public async Task<MaintenanceSchedule> AddAsync(MaintenanceSchedule entity, CancellationToken cancellationToken)
    {
        await _context.MaintenanceSchedules.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<IReadOnlyList<MaintenanceSchedule>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.MaintenanceSchedules.ToListAsync(cancellationToken);
    }

    public async Task<MaintenanceSchedule> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.MaintenanceSchedules.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task UpdateAsync(MaintenanceSchedule entity, CancellationToken cancellationToken)
    {
        _context.MaintenanceSchedules.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.MaintenanceSchedules.FindAsync(new object[] { id }, cancellationToken);
        if (entity != null)
        {
            _context.MaintenanceSchedules.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
