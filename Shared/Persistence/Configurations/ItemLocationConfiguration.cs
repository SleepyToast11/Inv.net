using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Persistence.Entities.Item;

namespace Shared.Persistence.Configurations;

public class ItemLocationConfiguration : IEntityTypeConfiguration<ItemLocationEntity>
{
    public void Configure(EntityTypeBuilder<ItemLocationEntity> builder)
    {
        builder.HasKey(il => new { il.ItemId, il.LocationId });

        builder.HasOne(il => il.Item)
            .WithMany(i => i.ItemLocations)
            .HasForeignKey(il => il.ItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(il => il.Location)
            .WithMany()
            .HasForeignKey(il => il.LocationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(il => il.Quantity)
            .IsRequired();
    }
}