namespace invProj.models;

public class TagSearchDocument
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string GroupName { get; set; } = null!;
    
    public TagSearchDocument ToSearchDocument(Tag tag)
    {
        return new TagSearchDocument
        {
            Id = tag.Id,
            Name = tag.Name,
            GroupName = tag.Group.Name
        };
    }
}