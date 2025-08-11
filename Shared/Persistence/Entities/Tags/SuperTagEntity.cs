using System.ComponentModel.DataAnnotations;
using Shared.Persistence.Entities.Common.Interfaces;

namespace Shared.Persistence.Entities.Tags;

public class SuperTagEntity : IEntity, ITenantable
{
    [MaxLength(100)] public string Name { get; set; } = null!;

    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
}