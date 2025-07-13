using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace invProj.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<ItemLocation> ItemLocations { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<ItemTag> ItemTags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Composite key for ItemLocation
        modelBuilder.Entity<ItemLocation>()
            .HasKey(il => new { il.ItemId, il.LocationId });

        modelBuilder.Entity<ItemLocation>()
            .HasOne(il => il.Item)
            .WithMany(i => i.ItemLocations)
            .HasForeignKey(il => il.ItemId);

        modelBuilder.Entity<ItemLocation>()
            .HasOne(il => il.Location)
            .WithMany(l => l.ItemLocations)
            .HasForeignKey(il => il.LocationId);

        // Composite key for ItemTag (many-to-many for tags)
        modelBuilder.Entity<ItemTag>()
            .HasKey(it => new { it.ItemId, it.TagId });

        modelBuilder.Entity<ItemTag>()
            .HasOne(it => it.Item)
            .WithMany(i => i.ItemTags)
            .HasForeignKey(it => it.ItemId);

        modelBuilder.Entity<ItemTag>()
            .HasOne(it => it.Tag)
            .WithMany(t => t.ItemTags)
            .HasForeignKey(it => it.TagId);

        base.OnModelCreating(modelBuilder);
    }
}

public class Tenant
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required] [MaxLength(100)] public string Name { get; set; } = "";

    // Optional: navigation to items, tags, etc. if needed
    // public ICollection<Item> Items { get; set; }
}

public class Item
{
    public int Id { get; set; }

    [Required] public Guid TenantId { get; set; }

    public Tenant Tenant { get; set; }

    [Required] [MaxLength(100)] public string Name { get; set; } = "";

    public ICollection<ItemLocation> ItemLocations { get; set; } = new List<ItemLocation>();
    public ICollection<ItemTag> ItemTags { get; set; } = new List<ItemTag>();

    [NotMapped] public int TotalQuantity => ItemLocations?.Sum(il => il.Quantity) ?? 0;
}

public class Location
{
    public int Id { get; set; }

    [Required] [MaxLength(100)] public string Name { get; set; } = "";

    public ICollection<ItemLocation> ItemLocations { get; set; } = new List<ItemLocation>();
}

public class ItemLocation
{
    public int ItemId { get; set; }
    public Item Item { get; set; }

    public int LocationId { get; set; }
    public Location Location { get; set; }

    public int Quantity { get; set; }
}

public class Tag
{
    public int Id { get; set; }

    [Required] [MaxLength(50)] public string Name { get; set; } = "";

    public Guid TenantId { get; set; }
    public Tenant Tenant { get; set; }

    public ICollection<ItemTag> ItemTags { get; set; } = new List<ItemTag>();
}

public class ItemTag
{
    public int ItemId { get; set; }
    public Item Item { get; set; }

    public int TagId { get; set; }
    public Tag Tag { get; set; }
}