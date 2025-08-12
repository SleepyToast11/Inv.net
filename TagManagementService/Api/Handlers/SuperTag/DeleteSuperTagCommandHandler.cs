using MediatR;
using TagManagementService.Application.Command;
using TagManagementService.Infrastructure.UnitOfWork;

namespace TagManagementService.Api.Handlers.SuperTag;

public class DeleteSuperTagCommandHandler(ITagUnitOfWork tagUnitOfWork) : IRequestHandler<DeleteSuperTagCommand, bool>
{
    public async Task<bool> Handle(DeleteSuperTagCommand request, CancellationToken cancellationToken)
    {
        var deletionSuccess = await tagUnitOfWork.SuperTags.DeleteAsync(request.Id, cancellationToken);
        return deletionSuccess;
    }
}