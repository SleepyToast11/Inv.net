using MediatR;
using Shared.Persistence.Repositories.Common.Interfaces;

namespace Shared.Api;

public abstract class PaginatedQuery<TResponse> : IRequest<TResponse>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}