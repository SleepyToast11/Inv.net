using MediatR;
using Shared.Domain.Tags;
using TagManagementService.Application.Command;
using TagManagementService.Infrastructure.Dto;
using TagManagementService.Infrastructure.UnitOfWork;

namespace TagManagementService.Api.Handlers.SuperTag;

public class CreateSuperTagCommandHandler: IRequestHandler<CreateSuperTagCommand, Guid>
{
    private readonly ITagUnitOfWork _tagUnitOfWork;

    public CreateSuperTagCommandHandler(ITagUnitOfWork tagUnitOfWork) => _tagUnitOfWork = tagUnitOfWork;
 
    
    public async Task<Guid> Handle(CreateSuperTagCommand request, CancellationToken cancellationToken)
    {
        var superTag = SuperTagFactory.Create(request.TenantId, request.Name);
        var superTagEntity = superTag.ToEntity();
        await _tagUnitOfWork.SuperTags.AddAsync(superTagEntity, cancellationToken);
        
        return superTag.Id;
    }
}