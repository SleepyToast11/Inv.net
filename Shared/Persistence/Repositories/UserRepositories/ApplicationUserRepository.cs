using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.ApplicationUser;
using Shared.Domain.ApplicationUser.Repositories;
using Shared.Persistence.Entities.ApplicationUser;
using Shared.Persistence.Repositories.Common.Generics;
using Shared.Security;

namespace Shared.Persistence.Repositories.UserRepositories;

public class ApplicationUserRepository
    : GenericRepository<ApplicationUserEntity>, IApplicationUserRepository
{
    private readonly IReadOnlyCollection<Guid> _readTenant;
    private readonly IReadOnlyCollection<Guid> _writeTenant;
    private readonly IReadOnlyCollection<Guid> _adminTenant;
    private readonly bool _superAdmin;

    public readonly string Scope = "UserManagement";

    public ApplicationUserRepository(AppDbContext context, ICurrentTenantAccess tenantAccess) : base(context.Users)
    {
        var writable = tenantAccess.WritableTenants.TryGetValue(Scope, out var w)
            ? w
            : Array.Empty<Guid>();

        var admin = tenantAccess.AdminTenants.TryGetValue(Scope, out var a)
            ? a
            : Array.Empty<Guid>();

        var readable = tenantAccess.ReadableTenants.TryGetValue(Scope, out var r)
            ? r
            : Array.Empty<Guid>();

        _writeTenant = new List<Guid>(writable);
        _readTenant = new List<Guid>(readable);
        _adminTenant = new List<Guid>(admin);
        _superAdmin = tenantAccess.SuperAdmin;
    }

    public override IQueryable<ApplicationUserEntity> ReadBaseQuery(bool deepLoad,
        Expression<Func<ApplicationUserEntity, bool>>? whereFilter = null)
    {
        if (whereFilter == null)
            whereFilter = DefaultExtraFilter;

        var query = base.ReadBaseQuery(deepLoad, whereFilter);

        if (!_superAdmin)
            query = query.Where(x =>
                x.UserPermissionEntities.Exists(p =>
                    _readTenant.Contains(p.TenantId)));
        return query;
    }

    public override IQueryable<ApplicationUserEntity> WriteBaseQuery(
        Expression<Func<ApplicationUserEntity, bool>>? whereFilter = null)
    {
        if (whereFilter == null)
            whereFilter = DefaultExtraFilter;

        var query = base.WriteBaseQuery(whereFilter);

        if (!_superAdmin)
            query = query.Where(x =>
                x.UserPermissionEntities.Exists(p =>
                    _writeTenant.Contains(p.TenantId)));
        return query;
    }

    public override IQueryable<ApplicationUserEntity> ApplyIncludes(IQueryable<ApplicationUserEntity> query)
    {
        return query
            .Include(au => au.UserPermissionEntities)
            .ThenInclude(up => up.Permissions);
    }

    public async Task<IReadOnlyList<ApplicationUserEntity>> GetAllUsersByTenant(Guid tenantId)
    {
        return await ReadBaseQuery(true, x =>
                x.UserPermissionEntities.Exists(p => p.TenantId == tenantId))
            .ToListAsync();
    }
}