using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Application.Common.Settings;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Persistence.Repositories;

public class ConditionerTypeRepository : IConditionerTypeRepository, IConditionerTypeQueries
{
    private readonly ApplicationDbContext _context;

    public ConditionerTypeRepository(ApplicationDbContext context, ApplicationSettings settings)
    {
        var connectionString = settings.ConnectionStrings.DefaultConnection;
        _context = context;
    }

    public async Task<ConditionerType> AddAsync(ConditionerType entity, CancellationToken cancellationToken)
    {
        await _context.ConditionerTypes.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<IReadOnlyList<ConditionerType>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.ConditionerTypes.ToListAsync(cancellationToken);
    }

    public async Task<ConditionerType> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.ConditionerTypes.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task UpdateAsync(ConditionerType entity, CancellationToken cancellationToken)
    {
        _context.ConditionerTypes.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _context.ConditionerTypes.FindAsync(new object[] { id }, cancellationToken);
        if (entity != null)
        {
            _context.ConditionerTypes.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
