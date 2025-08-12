using MediatR;
using TagManagementService.Application.Command;
using TagManagementService.Infrastructure.UnitOfWork;

namespace TagManagementService.Api.Handlers.SuperTag;

public class RenameTagCommandHAndler: IRequestHandler<RenameSuperTagCommand, bool>
{
    private readonly ITagUnitOfWork _tagUnitOfWork;

    public RenameTagCommandHAndler(ITagUnitOfWork tagUnitOfWork) => _tagUnitOfWork = tagUnitOfWork;

    public async Task<bool> Handle(RenameSuperTagCommand request, CancellationToken cancellationToken)
    {
        return await _tagUnitOfWork.SuperTags.UpdateAsync(request.id, entity =>
        {
            var superTag = new Shared.Domain.Tags.SuperTag(entity);
            superTag.Rename(request.NewName);
        },  cancellationToken);
    }
}