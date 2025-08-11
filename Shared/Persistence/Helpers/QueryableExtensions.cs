using Microsoft.EntityFrameworkCore;
using Shared.Persistence.Repositories.Common;
using Shared.Persistence.Repositories.Common.Interfaces;

namespace Shared.Persistence.Helpers;

public static class QueryableExtensions
{
    public static async Task<IPagedResult<T>> ToPagedResultAsync<T>(
        this IQueryable<T> query,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        if (page <= 0) page = 1;
        if (pageSize <= 0) pageSize = 10;

        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

        return new PagedResult<T>(items, totalCount, page, pageSize);
    }

    public static async Task<IPagedResult<T>> ToPagedResultAsync<T>(
        this IQueryable<T> query,
        PageRequest pageRequest,
        CancellationToken cancellationToken = default
    )
    {
        return await ToPagedResultAsync(query, pageRequest.Page, pageRequest.PageSize, cancellationToken);
    }
}