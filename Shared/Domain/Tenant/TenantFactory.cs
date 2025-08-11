using Shared.Persistence.Entities.Tenant;

namespace Shared.Domain.Tenant;

public static class TenantFactory
{
    public static Tenant Create(string name)
    {
        var entity = new TenantEntity
        {
            Id = Guid.NewGuid(),
            Name = name ?? throw new ArgumentNullException(nameof(name))
        };
        return new Tenant(entity);
    }
}