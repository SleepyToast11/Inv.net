using MediatR;
using Shared.Domain.Tags;
using Shared.Domain.Tags.Repositories;
using TagManagementService.Application.Command;
using TagManagementService.Infrastructure.UnitOfWork;

namespace TagManagementService.Api.Handlers.Tags;

public class RenameTagCommandHandler : IRequestHandler<RenameTagCommand, bool>
{
    private readonly ITagUnitOfWork _tagUnitOfWork;

    public RenameTagCommandHandler(ITagUnitOfWork tagUnitOfWork) => _tagUnitOfWork = tagUnitOfWork;
    
    public async Task<bool> Handle(RenameTagCommand request, CancellationToken cancellationToken)
    {
        return await _tagUnitOfWork.Tags.UpdateAsync(request.TagId, tagEntity => 
        {
            var tag = new Tag(tagEntity);
            tag.Rename(request.NewName);
        } ,cancellationToken);
    }
}
