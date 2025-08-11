using Shared.Persistence.Entities.Item;

namespace Shared.Domain.Item;

public static class ItemMapper
{
    public static Item ToDomain(ItemEntity entity)
    {
        return new Item(entity);
    }

    public static ItemEntity ToEntity(Item domain)
    {
        return domain.ToEntity();
    }
}