namespace invProj.models.Search;

public static class SearchDocumentMapper
{
    public static ItemSearchDocument ToSearchDocument(Item item) => new ItemSearchDocument
    {
        Id = item.Id,
        Name = item.Name,
        Quantity = item.Quantity,
        GroupName = item.Group.Name,
        UnitName = item.UnitName,
        Tags = item.ItemTags.Select(t => t.Tag.Name).ToList(),
        CreatedAt = item.CreatedAt,
        UpdatedAt = item.UpdatedAt,
        ShareType = item.ShareType.ToString()
    };

    public static TagSearchDocument ToSearchDocument(Tag tag) => new TagSearchDocument
    {
        Id = tag.Id,
        Name = tag.Name,
        GroupName = tag.Group.Name
    };
}