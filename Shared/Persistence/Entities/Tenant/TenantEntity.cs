using Shared.Persistence.Entities.Common.Interfaces;

namespace Shared.Persistence.Entities.Tenant;

public class TenantEntity : IEntity
{
    public string Name { get; set; } = null!;
    public Guid Id { get; set; }
}