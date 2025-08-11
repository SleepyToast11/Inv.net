using MediatR;
using Shared.Persistence.Repositories.Common.Interfaces;

namespace Shared.Api;


public class UnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
{
    private readonly IUnitOfWork _uow;

    public UnitOfWorkBehavior(IUnitOfWork uow)
        {
            _uow = uow;
        }

    public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            // Let the handler run
            var response = await next();

            // Only save for commands (not queries)
            if (typeof(TRequest).Name.EndsWith("Command", StringComparison.OrdinalIgnoreCase))
            {
                await _uow.SaveChangesAsync(cancellationToken);
            }

            return response;
        }
}
    