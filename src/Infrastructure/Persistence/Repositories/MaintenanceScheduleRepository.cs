using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Application.Common.Settings;
using Domain.Conditioners;
using Domain.MaintenanceSchedules;
using LanguageExt;
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

    public async Task<IReadOnlyList<MaintenanceSchedule>> GetByConditionerIdAsync(
    ConditionerId conditionerId,
    CancellationToken cancellationToken)
    {
        return await _context.MaintenanceSchedules
            .Include(x => x.Conditioner)
            .Where(x => x.ConditionerId == conditionerId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<MaintenanceSchedule> AddAsync(MaintenanceSchedule entity, CancellationToken cancellationToken)
    {
        await _context.MaintenanceSchedules.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<MaintenanceSchedule> UpdateAsync(MaintenanceSchedule entity, CancellationToken cancellationToken)
    {
        _context.MaintenanceSchedules.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<MaintenanceSchedule> DeleteAsync(MaintenanceSchedule entity, CancellationToken cancellationToken)
    {
        _context.MaintenanceSchedules.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Option<MaintenanceSchedule>> GetByIdAsync(MaintenanceScheduleId id, CancellationToken cancellationToken)
    {
        var entity = await _context.MaintenanceSchedules
            .Include(x => x.Conditioner)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);

        return entity ?? Option<MaintenanceSchedule>.None;
    }

    public async Task<IReadOnlyList<MaintenanceSchedule>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.MaintenanceSchedules
            .Include(x => x.Conditioner)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
