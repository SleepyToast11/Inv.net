using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared.Persistence.Entities.ApplicationUser;
using Shared.Persistence.Entities.Item;
using Shared.Persistence.Entities.Location;
using Shared.Persistence.Entities.Reservation;
using Shared.Persistence.Entities.Tags;
using Shared.Persistence.Entities.Tenant;

namespace Shared.Persistence;

public class AppDbContext : IdentityDbContext<ApplicationUserEntity, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<ItemEntity> Items { get; set; }
    public DbSet<ItemLocationEntity> ItemLocations { get; set; }


    public DbSet<ReservationEntity> Reservations { get; set; }
    public DbSet<ReservationItemEntity> ReservationItems { get; set; }

    public DbSet<TagEntity> Tags { get; set; }
    public DbSet<TagItemEntity> TagItems { get; set; }

    public DbSet<SuperTagEntity> SuperTags { get; set; }

    public DbSet<LocationEntity> Locations { get; set; }

    public DbSet<TenantEntity> Tenants { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all configurations from the assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}