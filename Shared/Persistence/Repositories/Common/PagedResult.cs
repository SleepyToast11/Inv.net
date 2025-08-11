using Shared.Persistence.Repositories.Common.Interfaces;

namespace Shared.Persistence.Repositories.Common;

public class PagedResult<T> : IPagedResult<T>
{
    public PagedResult(IEnumerable<T> items, int totalCount, int page, int pageSize)
    {
        Items = items.ToList().AsReadOnly();
        TotalCount = totalCount;
        Page = page;
        PageSize = pageSize;
    }

    public IReadOnlyList<T> Items { get; }
    public int TotalCount { get; }
    public int Page { get; }
    public int PageSize { get; }
}