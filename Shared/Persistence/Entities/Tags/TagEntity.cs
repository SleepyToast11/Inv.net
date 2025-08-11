using Shared.Persistence.Entities.Common.Interfaces;

namespace Shared.Persistence.Entities.Tags;

public class TagEntity : IEntity, ITenantable
{
    public string Name { get; set; } = default!;

    public Guid SuperTagId { get; set; }
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }
}