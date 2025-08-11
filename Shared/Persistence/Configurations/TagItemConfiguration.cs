using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Persistence.Entities.Tags;

namespace Shared.Persistence.Configurations;

public class TagItemConfiguration : IEntityTypeConfiguration<TagItemEntity>
{
    public void Configure(EntityTypeBuilder<TagItemEntity> builder)
    {
        builder.HasKey(ti => new { ti.ItemId, ti.TagId });

        builder.HasOne(ti => ti.Item)
            .WithMany(i => i.TagItems)
            .HasForeignKey(ti => ti.ItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ti => ti.Tag)
            .WithMany()
            .HasForeignKey(ti => ti.TagId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}