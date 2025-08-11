using MediatR;
using TagManagementService.Application.Command;
using TagManagementService.Infrastructure.UnitOfWork;

namespace TagManagementService.Api.Handlers.Tags;

public class DeleteTagCommandHandler: IRequestHandler<DeleteTagCommand>
{
    private readonly ITagUnitOfWork _tagUnitOfWork;
    
    public DeleteTagCommandHandler(ITagUnitOfWork tagUnitOfWork) => _tagUnitOfWork = tagUnitOfWork;

    public async Task Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        await _tagUnitOfWork.Tags.DeleteAsync(request.id, cancellationToken);
    }
}