namespace Shared.Persistence.Repositories.Common.Interfaces;

public interface IPagedResult<T>
{
    IReadOnlyList<T> Items { get; }
    int TotalCount { get; }
    int Page { get; }
    int PageSize { get; }
}