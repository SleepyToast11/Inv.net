using Shared.Persistence.Entities.Tags;

namespace Shared.Domain.Tags;

public class TagItemFactory
{
    public static TagItem Create(Guid itemId, Guid tagId)
    {
        var entity = new TagItemEntity
        {
            ItemId = itemId,
            TagId = tagId
        };

        return new TagItem(entity);
    }
}