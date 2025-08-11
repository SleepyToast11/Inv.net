using Shared.Persistence.Entities.Item;
using Shared.Persistence.Entities.Reservation;
using Shared.Persistence.Entities.Tags;

namespace Shared.Domain.Item;

/// <summary>
/// Factory for creating new Item domain aggregates.
/// Keeps EF entity creation logic in one place.
/// </summary>
public class ItemFactory
{
    /// <summary>
    /// Creates a new Item aggregate for a given tenant.
    /// </summary>
    public static Item Create(string name, Guid tenantId)
    {
        var entity = new ItemEntity
        {
            Id = Guid.NewGuid(),
            TenantId = tenantId,
            Name = name,
            ItemLocations = new List<ItemLocationEntity>(),
            ReservationItems = new List<ReservationItemEntity>(),
            TagItems = new List<TagItemEntity>()
        };

        return new Item(entity);
    }
}