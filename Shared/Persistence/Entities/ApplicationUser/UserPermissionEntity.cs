using Shared.Persistence.Entities.Tenant;

namespace Shared.Persistence.Entities.ApplicationUser;

public class UserPermissionEntity
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public TenantEntity TenantEntity { get; set; } = null!;

    public List<PermissionEntity> Permissions { get; set; } = new();
}