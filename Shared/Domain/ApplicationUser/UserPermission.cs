using System.Collections.ObjectModel;
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

    public ReadOnlyDictionary<string, PermissionLevel> Permissions => _permissions.AsReadOnly();

    public void AddPermission(string scope, PermissionLevel level)
    {
        var index = _entity.Permissions.FindIndex(p => p.Scope == scope);
        if (index >= 0)
            throw new Exception($"Permission {scope} is already created");

        //probably will only be created here, no need to go insane
        var permissionEntity = new PermissionEntity { Id = Guid.NewGuid(), Scope = scope, Level = level };
        
        _entity.Permissions.Add(permissionEntity);
        
        _permissions[scope] = level;
    }
    
    public void UpdatePermission(string scope, PermissionLevel level)
    {
        var index = _entity.Permissions.FindIndex(p => p.Scope == scope);
        if (index < 0)
            throw new Exception($"Permission {scope} does not exist");
        
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