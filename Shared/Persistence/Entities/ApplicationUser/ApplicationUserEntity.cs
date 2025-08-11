using Microsoft.AspNetCore.Identity;
using Shared.Persistence.Entities.Common.Interfaces;

namespace Shared.Persistence.Entities.ApplicationUser;

public class ApplicationUserEntity : IdentityUser<Guid>, IEntity
{
    public List<UserPermissionEntity> UserPermissionEntities { get; set; } = new();
}