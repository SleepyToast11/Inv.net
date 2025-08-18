using Shared.Domain.ApplicationUser;

namespace UserManagementService.Infrastructure.Dto;

public record UserApplicationDto
{
    public Guid Id;
    public IReadOnlyList<Guid> TenantIds;

    public UserApplicationDto(Guid id, IReadOnlyList<Guid> tenantIds)
    {
        Id = id;
        TenantIds = tenantIds;
    }
    
    public UserApplicationDto(ApplicationUser user)
    {
        Id = user.Id;
        TenantIds = user.TenantPermissions.Select(tp => tp.TenantId).ToList();
    }
}