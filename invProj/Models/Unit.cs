using System.ComponentModel.DataAnnotations;

namespace invProj.models;

public class Unit
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required] public string Name { get; set; } = null!;
}