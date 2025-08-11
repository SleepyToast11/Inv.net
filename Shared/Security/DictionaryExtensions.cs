namespace Shared.Security;

internal static class DictionaryExtensions
{
    public static HashSet<Guid> GetOrAdd(this Dictionary<string, HashSet<Guid>> dict, string scope)
    {
        if (!dict.TryGetValue(scope, out var set))
        {
            set = new HashSet<Guid>();
            dict[scope] = set;
        }
        return set;
    }
}
