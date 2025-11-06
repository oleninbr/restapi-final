using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Application.Common.Settings;
using Domain.ConditionerTypes;
using LanguageExt;
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

    public async Task<Option<ConditionerType>> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        var entity = await _context.ConditionerTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name == name, cancellationToken);

        return entity ?? Option<ConditionerType>.None;
    }


    public async Task<ConditionerType> AddAsync(ConditionerType entity, CancellationToken cancellationToken)
    {
        await _context.ConditionerTypes.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<ConditionerType> UpdateAsync(ConditionerType entity, CancellationToken cancellationToken)
    {
        _context.ConditionerTypes.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<ConditionerType> DeleteAsync(ConditionerType entity, CancellationToken cancellationToken)
    {
        _context.ConditionerTypes.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Option<ConditionerType>> GetByIdAsync(ConditionerTypeId id, CancellationToken cancellationToken)
    {
        var entity = await _context.ConditionerTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);

        return entity ?? Option<ConditionerType>.None;
    }

    public async Task<IReadOnlyList<ConditionerType>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.ConditionerTypes
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
