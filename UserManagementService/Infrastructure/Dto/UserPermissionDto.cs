using Shared.Domain.ApplicationUser;
using Shared.Persistence.Entities.ApplicationUser;

namespace UserManagementService.Infrastructure.Dto;

public record UserPermissionDto
{
    public IReadOnlyDictionary<string, PermissionLevel>? PermissionScopes { get; init; }

    public UserPermissionDto(Dictionary<string, PermissionLevel> permissionScopes)
    {
        PermissionScopes = permissionScopes;
    }

    public UserPermissionDto(ApplicationUser user, Guid tenantId)
    {
        user = user ?? throw new ArgumentNullException(nameof(user));
        PermissionScopes = user
            .TenantPermissions?
            .FirstOrDefault(tp => tp.TenantId == tenantId)?.Permissions 
                           ?? new Dictionary<string, PermissionLevel>().AsReadOnly();
    }
}