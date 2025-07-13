namespace invProj.models;

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

    public ItemSearchDocument ToSearchDocument(Item item)
    {
        return new ItemSearchDocument
        {
            Id = item.Id,
            Name = item.Name,
            Quantity = item.Quantity,
            GroupName = item.Group.Name,
            UnitName = item.UnitName,
            Tags = item.ItemTags.Select(t => t.Tag.Name).ToList(),
            CreatedAt = item.CreatedAt,
            UpdatedAt = item.UpdatedAt,
            ShareType = item.ShareType.ToString()  // Enum to string
        };
    }
}