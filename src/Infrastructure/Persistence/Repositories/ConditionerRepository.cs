using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Application.Common.Settings;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ConditionerRepository : IConditionerRepository, IConditionerQueries
{
    private readonly ApplicationDbContext _context;

    public ConditionerRepository(ApplicationDbContext context, ApplicationSettings settings)
    {
        var connectionString = settings.ConnectionStrings.DefaultConnection; // приклад використання

        _context = context;
    }

    public async Task<Conditioner> AddAsync(Conditioner entity, CancellationToken cancellationToken)
    {
        await _context.Conditioners.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<IReadOnlyList<Conditioner>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Conditioners.ToListAsync(cancellationToken);
    }

    public async Task<Conditioner> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Conditioners.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task UpdateAsync(Conditioner entity, CancellationToken cancellationToken)
    {
        _context.Conditioners.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.Conditioners.FindAsync(new object[] { id }, cancellationToken);
        if (entity != null)
        {
            _context.Conditioners.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
