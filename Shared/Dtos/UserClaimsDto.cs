namespace Shared.Dtos;

public class UserClaimsDto
{
    public Guid Sub { get; set; } // maps to "sub"
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public List<TenantPermissionClaim> TenantPermissions { get; set; } = new();
}