using Shared.Persistence.Entities.Common.Interfaces;

namespace Shared.Persistence.Entities.Location;

public class LocationEntity : IEntity, ITenantable
{
    public string Name { get; set; } = null!;
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
}