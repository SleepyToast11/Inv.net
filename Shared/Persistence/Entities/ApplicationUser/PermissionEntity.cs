namespace Shared.Persistence.Entities.ApplicationUser;

public class PermissionEntity
{
    public Guid Id { get; set; }
    public required string Scope { get; set; }
    public PermissionLevel Level { get; set; }
}