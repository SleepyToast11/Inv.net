using System.ComponentModel.DataAnnotations;

namespace invProj.models;

public class Location
{
    public int Id { get; set; } 

    [MaxLength(100)] public string Name { get; set; } = "";

    [Required]
    public Group Group { get; set; } = null!;
}