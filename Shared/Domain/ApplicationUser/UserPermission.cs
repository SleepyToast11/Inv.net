using Shared.Persistence.Entities.ApplicationUser;

namespace Shared.Domain.ApplicationUser;

public class UserPermission
{
    private readonly UserPermissionEntity _entity;

    public UserPermission(UserPermissionEntity entity)
    {
        _entity = entity ?? throw new ArgumentNullException(nameof(entity));
    }

    public Guid TenantId => _entity.TenantId;

    private Dictionary<string, PermissionLevel> _permissions => _entity.Permissions
        .ToDictionary(
            g => g.Scope,
            g => g.Level
        );

    public IReadOnlyDictionary<string, PermissionLevel> Permissions => _permissions.AsReadOnly();

    public void SetPermission(string scope, PermissionLevel level)
    {
        var index = _entity.Permissions.FindIndex(p => p.Scope == scope);
        if (index >= 0)
            _entity.Permissions[index].Level = level;

        _permissions[scope] = level;
    }

    public bool RemovePermission(string scope)
    {
        var index = _entity.Permissions.FindIndex(p => p.Scope == scope);
        if (index >= 0)
        {
            _entity.Permissions.RemoveAt(index);
            return true;
        }

        return false;
    }

    public bool TryGetPermission(string scope, out PermissionLevel level)
    {
        return _permissions.TryGetValue(scope, out level);
    }

    public bool HasPermission(string scope, PermissionLevel requiredLevel)
    {
        return _permissions.TryGetValue(scope, out var currentLevel)
               && currentLevel >= requiredLevel;
    }

    internal UserPermissionEntity Unwrap()
    {
        return _entity;
    }
}