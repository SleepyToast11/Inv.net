namespace invProj.models.JoinEntities;

public class ItemLocation
{
    public int ItemId { get; set; }
    public Item Item { get; set; }

    public int LocationId { get; set; }
    public Location Location { get; set; }
    
    public int Quantity { get; set; }
    public string UnitName { get; set; } = "Un";
    
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
}