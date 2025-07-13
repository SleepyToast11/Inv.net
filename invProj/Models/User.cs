namespace invProj.models;

public enum UserType
{
    Admin,
    GroupAdmin,
    User
}

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public UserType UserType { get; set; }
}