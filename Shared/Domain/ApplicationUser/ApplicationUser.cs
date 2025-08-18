using Shared.Persistence.Entities.ApplicationUser;
using Shared.Persistence.Entities.Common.Interfaces;

namespace Shared.Domain.ApplicationUser;

public class ApplicationUser : IEntity
{
    private readonly ApplicationUserEntity _entity;

    private readonly List<UserPermission> _tenantPermissions;

    public ApplicationUser(ApplicationUserEntity entity)
    {
        _entity = entity ?? throw new ArgumentNullException(nameof(entity));

        _tenantPermissions = _entity.UserPermissionEntities
            .Select(p => new UserPermission(p))
            .ToList();
    }

    public string Email => _entity.Email!;
    public string? UserName => _entity.UserName;
    public IReadOnlyCollection<UserPermission> TenantPermissions => _tenantPermissions.AsReadOnly();

    public Guid Id => _entity.Id;

    public UserPermission? GetTenantPermission(Guid tenantId)
    {
        return _tenantPermissions.FirstOrDefault(p => p.TenantId == tenantId);
    }

    public UserPermission CreateTenantPermission(Guid tenantId)
    {
        var existing = GetTenantPermission(tenantId);
        if (existing != null)
            throw new Exception($"Tenant {tenantId} is already created");
        
        var entity = new UserPermissionEntity
        {
            TenantId = tenantId,
            Permissions = new List<PermissionEntity>()
        };
        _entity.UserPermissionEntities.Add(entity);

        var wrapped = new UserPermission(entity);
        _tenantPermissions.Add(wrapped);
        return wrapped;
    }

    public bool RemoveTenantPermission(Guid tenantId)
    {
        var domain = GetTenantPermission(tenantId);
        if (domain == null) return false;

        var removedFromEntity = _entity.UserPermissionEntities.Remove(domain.Unwrap());
        _tenantPermissions.Remove(domain);
        return removedFromEntity;
    }

    internal ApplicationUserEntity Unwrap()
    {
        return _entity;
    }
}