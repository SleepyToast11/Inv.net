using Shared.Persistence.Entities.Location;

namespace Shared.Persistence.Entities.Item;

public class ItemLocationEntity
{
    public ItemEntity Item { get; set; } = null!;
    public Guid ItemId { get; set; }

    public LocationEntity Location { get; set; }
    public Guid LocationId { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}