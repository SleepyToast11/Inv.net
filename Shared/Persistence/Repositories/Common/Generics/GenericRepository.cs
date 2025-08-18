using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Common.Interfaces;
using Shared.Persistence.Entities.Common.Interfaces;
using Shared.Persistence.Helpers;
using Shared.Persistence.Repositories.Common.Interfaces;

namespace Shared.Persistence.Repositories.Common.Generics;

public abstract class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
{
    private readonly DbSet<T> _context;

    protected GenericRepository(DbSet<T> context, Expression<Func<T, bool>>? whereFilter = null)
    {
        DefaultExtraFilter = whereFilter ?? (x => true);

        _context = context;
    }

    public Expression<Func<T, bool>> DefaultExtraFilter { get; set; }

    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken, bool deepLoad = true,
        Expression<Func<T, bool>>? whereFilter = null)
    {
        return await ReadBaseQuery(deepLoad, whereFilter)
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public virtual async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken, bool deepLoad = false,
        Expression<Func<T, bool>>? whereFilter = null)
    {
        return await ReadBaseQuery(deepLoad, whereFilter)
            .ToListAsync(cancellationToken);
    }

    public virtual async Task<IPagedResult<T>> GetPagedAsync(PageRequest pageRequest,
        CancellationToken cancellationToken = default, bool deepLoad = false,
        Expression<Func<T, bool>>? whereFilter = null)
    {
        return await ReadBaseQuery(deepLoad, whereFilter)
            .ToPagedResultAsync(pageRequest, cancellationToken);
    }

    public virtual async Task AddAsync(T t, CancellationToken cancellationToken)
    {
        var existingItem = await GetForWriteAsyncById(t.Id, cancellationToken);

        if (existingItem != null)
            throw new ApplicationException("Entity already exists");

        await _context.AddAsync(t, cancellationToken);
    }

    public virtual async Task<bool> UpdateAsync(Guid id, Action<T> updateAction, CancellationToken cancellationToken)
    {
        var item = await GetForWriteAsyncById(id, cancellationToken);

        if (item == null)
            return false;

        updateAction(item);

        return true;
    }

    public virtual async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var toDelete = await GetForWriteAsyncById(id, cancellationToken);

        if (toDelete == null)
            return false;
        _context.Remove(toDelete);
        return true;
    }

    public virtual IQueryable<T> ApplyIncludes(IQueryable<T> query)
    {
        return query;
    }


    public virtual IQueryable<T> ReadBaseQuery(bool deepLoad, Expression<Func<T, bool>>? whereFilter = null)
    {
        if (whereFilter == null)
            whereFilter = DefaultExtraFilter;

        var query = _context
            .Where(whereFilter);

        return deepLoad ? query : ApplyIncludes(query);
    }

    public virtual IQueryable<T> WriteBaseQuery(Expression<Func<T, bool>>? whereFilter = null)
    {
        if (whereFilter == null)
            whereFilter = DefaultExtraFilter;

        return ApplyIncludes(_context
            .Where(whereFilter)
        );
    }

    private async Task<T?> GetForWriteAsyncById(Guid id, CancellationToken cancellationToken)
    {
        return await WriteBaseQuery()
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }
}