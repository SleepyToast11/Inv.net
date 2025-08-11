using MediatR;
using Shared.Domain.Tags;
using Shared.Domain.Tags.Repositories;
using TagManagementService.Application.Command;
using TagManagementService.Infrastructure.UnitOfWork;

namespace TagManagementService.Api.Handlers.Tags;

public class CreateTagCommandHandler: IRequestHandler<CreateTagCommand, Guid>
{
    private readonly ITagUnitOfWork _tagUnitOfWork;
    
    public CreateTagCommandHandler(ITagUnitOfWork tagUnitOfWork) => _tagUnitOfWork = tagUnitOfWork;
    
    public async Task<Guid> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var tag = TagFactory.Create(request.Name, request.TenantId, request.SuperTagId);
        var tagEntity = tag.ToEntity();
        await _tagUnitOfWork.Tags.AddAsync(tagEntity, cancellationToken);

        return tag.Id;
    }
}