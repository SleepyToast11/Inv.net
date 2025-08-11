using Shared.Persistence.Entities.Item;

namespace Shared.Persistence.Entities.Tags;

public class TagItemEntity
{
    public Guid ItemId { get; set; }
    public ItemEntity? Item { get; set; }

    public Guid TagId { get; set; }
    public TagEntity? Tag { get; set; }
}