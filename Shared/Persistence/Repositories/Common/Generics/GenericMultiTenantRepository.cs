using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Shared.Persistence.Entities.Common.Extensions;
using Shared.Persistence.Entities.Common.Interfaces;
using Shared.Persistence.Helpers;
using Shared.Persistence.Repositories.Common.Interfaces;
using Shared.Security;

namespace Shared.Persistence.Repositories.Common.Generics;

//Feels like I'm a wizard, but I might just be loading the cannon pointed towards my foot. 
public abstract class GenericMultiTenantRepository<T> where T : class, ITenantable, IEntity
{
    private readonly DbSet<T> _context;
    private readonly IReadOnlyCollection<Guid> _readTenant;
    private readonly IReadOnlyCollection<Guid> _writeTenant;
    
    protected GenericMultiTenantRepository(DbSet<T> context, ICurrentTenantAccess tenantAccess, Expression<Func<T, bool>>? whereFilter = null)
    {
        DefaultExtraFilter = whereFilter ?? (x => true);
        
        _context = context;
        
        var scope = TenantableExtensions.GetScope<T>();

        var writable = tenantAccess.WritableTenants.TryGetValue(scope, out var w)
            ? w
            : Array.Empty<Guid>();
        
        var readable = tenantAccess.ReadableTenants.TryGetValue(scope, out var r)
            ? r
            : Array.Empty<Guid>();

        _writeTenant = new List<Guid>(writable);
        _readTenant = new List<Guid>(readable);
    }

    public Expression<Func<T, bool>> DefaultExtraFilter { get; set; }

    public virtual IQueryable<T> ApplyIncludes(IQueryable<T> query)
    {
        return query;
    }
    
    public IQueryable<T> ReadBaseQuery(bool deepLoad, Expression<Func<T, bool>>? whereFilter = null)
    {
        if (whereFilter == null)
            whereFilter = DefaultExtraFilter;

        var query = _context
            .Where(t => _readTenant.Contains(t.TenantId))
            .Where(whereFilter);

        return deepLoad ? query : ApplyIncludes(query);
    }

    public IQueryable<T> WriteBaseQuery(Expression<Func<T, bool>>? whereFilter = null)
    {
        if (whereFilter == null)
            whereFilter = DefaultExtraFilter;

        return ApplyIncludes(_context
            .Where(t => _writeTenant.Contains(t.TenantId))
            .Where(whereFilter)
        );
    }

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

    private async Task<T?> GetForWriteAsyncById(Guid id, CancellationToken cancellationToken)
    {
        return await WriteBaseQuery()
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public virtual async Task AddAsync(T t, CancellationToken cancellationToken)
    {
        var existingItem = await GetForWriteAsyncById(t.Id, cancellationToken);

        if (existingItem != null)
            throw new ApplicationException("Entity already exists");

        if (!_writeTenant.Contains(t.TenantId))
            throw new UnauthorizedAccessException("Not allowed to add entity with this tenant id");

        await _context.AddAsync(t, cancellationToken);
    }

    public virtual async Task<bool> UpdateAsync(Guid id, Action<T> updateAction, CancellationToken cancellationToken)
    {
        var item = await GetForWriteAsyncById(id, cancellationToken);

        if (item == null)
            return false;

        updateAction(item);

        if (!_writeTenant.Contains(item.TenantId)) //revalidate after update
            throw new UnauthorizedAccessException("Not allowed to update entity to this tenant");
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
}