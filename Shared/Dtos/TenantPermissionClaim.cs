namespace Shared.Dtos;

public class TenantPermissionClaim
{
    public Guid TenantId { get; set; }

    public Dictionary<string, string> Permissions { get; set; } = new();
}