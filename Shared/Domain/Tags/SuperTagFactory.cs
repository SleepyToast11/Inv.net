using Shared.Persistence.Entities.Tags;

namespace Shared.Domain.Tags;

public static class SuperTagFactory
{
    public static SuperTag Create(Guid tenantId, string name)
    {
        var entity = new SuperTagEntity
        {
            Id = Guid.NewGuid(),
            Name = name ?? throw new ArgumentNullException(nameof(name)),
            TenantId = tenantId
        };
        return new SuperTag(entity);
    }
}