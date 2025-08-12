using Shared.Domain.Location;

namespace ItemManagementService.Infrastructure.Dtos;

public record LocationDto(Guid Id, string Name, Guid TenantId)
{
    public LocationDto(Location location) : this(location.Id, location.Name, location.TenantId)
    {
    }
}