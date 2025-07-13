using System.ComponentModel.DataAnnotations;
using invProj.models.JoinEntities;


namespace invProj.models;

public class Tag
{
    public int Id { get; set; }

    [Required] [MaxLength(50)] public string Name { get; set; }

    public ICollection<ItemTag> ItemTags { get; set; } = new List<ItemTag>();

    
    public Group Group { get; set; } = null!;
}