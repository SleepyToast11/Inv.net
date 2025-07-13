using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using invProj.models.Enums;
using invProj.models.ExternalData;
using invProj.models.JoinEntities;

namespace invProj.models;

public class Item
{
    public Item()
    {
        //If no unit is set, set the default unit of the group
        Unit ??= Group.GroupUnit;
    }
    
    [Key] public int Id { get; set; } // Primary key

    [Required] [MaxLength(100)] public string Name { get; set; } // Item name

    [NotMapped] public int Quantity => ItemLocations?.Sum(il => il.Quantity) ?? 0;

    public Photo? ItemPhoto { get; set; }

    public ICollection<ItemLocation> ItemLocations { get; set; } = new List<ItemLocation>();

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow; // Creation timestamp

    public DateTime? UpdatedAt { get; set; } // Last update timestamp

    [Required] public Group Group { get; set; } = null!;
    
    public Unit Unit { get; set; }

    /// <summary>
    /// Not to be mixed with unit that is linked to scouts, it is measurment type
    /// </summary>
    public string UnitName { get; set; } = "Un";
    
    public ICollection<ItemTag> ItemTags { get; set; } = new List<ItemTag>();
    
    public ShareType ShareType { get; set; } = ShareType.Group;
}