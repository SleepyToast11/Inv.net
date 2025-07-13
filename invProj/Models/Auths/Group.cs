namespace invProj.models;

public class Group
{
    public Group()
    {
        GroupUnit = new Unit { Name = "group" };
        Units = new List<Unit> { GroupUnit };
    }
    
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public Unit GroupUnit { get; set; }
    public List<Unit> Units { get; set; }
}