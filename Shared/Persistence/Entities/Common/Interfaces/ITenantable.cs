namespace Shared.Persistence.Entities.Common.Interfaces;

public interface ITenantable
{
    public Guid TenantId { get; }
}