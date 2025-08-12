using MediatR;
using TagManagementService.Application.Command;
using TagManagementService.Infrastructure.UnitOfWork;

namespace TagManagementService.Api.Handlers.Tags;

public class DeleteTagCommandHandler: IRequestHandler<DeleteTagCommand, bool>
{
    private readonly ITagUnitOfWork _tagUnitOfWork;
    
    public DeleteTagCommandHandler(ITagUnitOfWork tagUnitOfWork) => _tagUnitOfWork = tagUnitOfWork;

    public async Task<bool> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var done = await _tagUnitOfWork.Tags.DeleteAsync(request.id, cancellationToken);
        return done;
    }
}