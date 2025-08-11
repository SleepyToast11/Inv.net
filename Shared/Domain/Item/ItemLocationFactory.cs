using Shared.Persistence.Entities.Item;

namespace Shared.Domain.Item;

public static class ItemLocationFactory
{
    public static ItemLocation Create(Guid itemId, Guid locationId, int quantity)
    {
        var entity = new ItemLocationEntity
        {
            ItemId = itemId,
            LocationId = locationId,
            Quantity = quantity,
            CreatedAt = DateTime.UtcNow
        };

        return new ItemLocation(entity);
    }
}