using Shared.Persistence.Entities.Common.Interfaces;

namespace Shared.Persistence.Entities.Common.Extensions;

public static class TenantableExtensions
{
    public static string GetScope<T>() where T : ITenantable
    {
        // For example, scope = lowercase class name, pluralized if you want
        var name = typeof(T).Name;

        // simple example: just lowercase
        return name.ToLowerInvariant();
    }
}
