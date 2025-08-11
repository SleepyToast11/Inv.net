namespace Shared.Security;

public interface ICurrentTenantAccess
{
    IReadOnlyDictionary<string, IReadOnlyCollection<Guid>> ReadableTenants { get; }
    IReadOnlyDictionary<string, IReadOnlyCollection<Guid>> WritableTenants { get; }
    IReadOnlyDictionary<string, IReadOnlyCollection<Guid>> AdminTenants { get; }

    bool HasReadAccess(string scope, Guid tenantId);
    bool HasWriteAccess(string scope, Guid tenantId);
    bool HasAdminAccess(string scope, Guid tenantId);
}


