using Shared.Persistence.Entities.Location;

namespace Shared.Domain.Location;

public static class LocationMapper
{
    public static Location ToDomain(LocationEntity entity)
    {
        return new Location(entity);
    }

    public static LocationEntity ToEntity(Location domain)
    {
        return domain.ToEntity();
    }
}