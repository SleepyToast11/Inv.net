namespace invProj.models.Search;

public class ItemSearchDocument
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Quantity { get; set; }
    public string? PhotoUrl { get; set; }
    public string GroupName { get; set; } = null!;
    public string UnitName { get; set; } = null!;
    public List<string> Tags { get; set; } = new List<string>();
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string ShareType { get; set; } = null!;
    
}