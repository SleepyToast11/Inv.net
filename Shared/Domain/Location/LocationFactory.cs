using Shared.Persistence.Entities.Location;

namespace Shared.Domain.Location;

public static class LocationFactory
{
    public static Location Create(string name, Guid tenantId)
    {
        var entity = new LocationEntity
        {
            Id = Guid.NewGuid(),
            Name = name ?? throw new ArgumentNullException(nameof(name)),
            TenantId = tenantId
        };
        return new Location(entity);
    }
}