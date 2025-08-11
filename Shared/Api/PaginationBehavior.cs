using MediatR;

namespace Shared.Api;

public class PaginationBehavior<TRequest, TResponse> 
    : IPipelineBehavior<TRequest, TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (request is PaginatedQuery<TResponse> paginated)
        {
            paginated.PageSize = Math.Min(paginated.PageSize, 100);
            paginated.PageNumber = Math.Max(paginated.PageNumber, 1);
        }

        return await next(cancellationToken);
    }
}