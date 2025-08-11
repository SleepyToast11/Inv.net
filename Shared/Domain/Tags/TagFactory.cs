using Shared.Persistence.Entities.Tags;

namespace Shared.Domain.Tags;

public static class TagFactory
{
    public static Tag Create(string name, Guid tenantId, Guid superTagId)
    {
        var entity = new TagEntity
        {
            Id = Guid.NewGuid(),
            Name = name,
            TenantId = tenantId,
            SuperTagId = superTagId
        };

        return new Tag(entity);
    }
}