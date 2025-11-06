using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Application.Common.Settings;
using Domain.Conditioners;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ConditionerRepository : IConditionerRepository, IConditionerQueries
{
    private readonly ApplicationDbContext _context;

    public ConditionerRepository(ApplicationDbContext context, ApplicationSettings settings)
    {
        // приклад використання конфігурації
        var connectionString = settings.ConnectionStrings.DefaultConnection;
        _context = context;
    }

    // -----------------------------
    // 🔹 CRUD METHODS (Repository)
    // -----------------------------

    public async Task<Conditioner> AddAsync(Conditioner entity, CancellationToken cancellationToken)
    {
        await _context.Conditioners.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Conditioner> UpdateAsync(Conditioner entity, CancellationToken cancellationToken)
    {
        _context.Conditioners.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Conditioner> DeleteAsync(Conditioner entity, CancellationToken cancellationToken)
    {
        _context.Conditioners.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    // -----------------------------
    // 🔹 QUERIES (IConditionerQueries)
    // -----------------------------

    public async Task<Option<Conditioner>> GetByIdAsync(ConditionerId id, CancellationToken cancellationToken)
    {
        var entity = await _context.Conditioners
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);

        return entity ?? Option<Conditioner>.None;
    }

    public async Task<Option<Conditioner>> GetBySerialNumberAsync(string serialNumber, CancellationToken cancellationToken)
    {
        var entity = await _context.Conditioners
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.SerialNumber == serialNumber, cancellationToken);

        return entity ?? Option<Conditioner>.None;
    }

    public async Task<IReadOnlyList<Conditioner>> GetAllAsync(CancellationToken cancellationToken)
    {
        // включаємо зв’язані сутності
        return await _context.Conditioners
            .Include(x => x.Status!)
            .Include(x => x.Type!)
            .Include(x => x.Manufacturer!)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
