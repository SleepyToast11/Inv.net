namespace Shared.Security;

public class CurrentTenantAccess : ICurrentTenantAccess
{
    private readonly Dictionary<string, HashSet<Guid>> _readable = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, HashSet<Guid>> _writable = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, HashSet<Guid>> _admin = new(StringComparer.OrdinalIgnoreCase);

    public IReadOnlyDictionary<string, IReadOnlyCollection<Guid>> ReadableTenants =>
        _readable.ToDictionary(kvp => kvp.Key, kvp => (IReadOnlyCollection<Guid>)kvp.Value);
    public IReadOnlyDictionary<string, IReadOnlyCollection<Guid>> WritableTenants =>
        _writable.ToDictionary(kvp => kvp.Key, kvp => (IReadOnlyCollection<Guid>)kvp.Value);
    public IReadOnlyDictionary<string, IReadOnlyCollection<Guid>> AdminTenants =>
        _admin.ToDictionary(kvp => kvp.Key, kvp => (IReadOnlyCollection<Guid>)kvp.Value);

    public void AddReadable(string scope, Guid tenantId) =>
        _readable.GetOrAdd(scope).Add(tenantId);

    public void AddWritable(string scope, Guid tenantId) =>
        _writable.GetOrAdd(scope).Add(tenantId);

    public void AddAdmin(string scope, Guid tenantId) =>
        _admin.GetOrAdd(scope).Add(tenantId);

    public bool HasReadAccess(string scope, Guid tenantId) =>
        _readable.TryGetValue(scope, out var set) && set.Contains(tenantId);

    public bool HasWriteAccess(string scope, Guid tenantId) =>
        _writable.TryGetValue(scope, out var set) && set.Contains(tenantId);

    public bool HasAdminAccess(string scope, Guid tenantId) =>
        _admin.TryGetValue(scope, out var set) && set.Contains(tenantId);
}
