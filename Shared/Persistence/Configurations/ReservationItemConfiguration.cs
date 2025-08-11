using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Persistence.Entities.Reservation;

namespace Shared.Persistence.Configurations;

public class ReservationItemConfiguration : IEntityTypeConfiguration<ReservationItemEntity>
{
    public void Configure(EntityTypeBuilder<ReservationItemEntity> builder)
    {
        builder.HasKey(ri => new { ri.ReservationId, ri.ItemId, ri.LocationId });

        builder.HasOne(ri => ri.Reservation)
            .WithMany(i => i.ReservationItems)
            .HasForeignKey(ri => ri.ReservationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ri => ri.Item)
            .WithMany(i => i.ReservationItems)
            .HasForeignKey(ri => ri.ItemId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ri => ri.Location).WithMany()
            .HasForeignKey(ri => ri.LocationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(ri => ri.Quantity)
            .IsRequired();
    }
}