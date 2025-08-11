using MediatR;
using TagManagementService.Application.Queries;
using TagManagementService.Infrastructure.Dto;
using TagManagementService.Infrastructure.UnitOfWork;

namespace TagManagementService.Api.Handlers.Tags;

public class GetTagByIdQueryHandler: IRequestHandler<GetTagByIdQuery, TagDto?>
{
    private readonly ITagUnitOfWork _tagUnitOfWork;
    
    public GetTagByIdQueryHandler(ITagUnitOfWork tagUnitOfWork) => _tagUnitOfWork = tagUnitOfWork;

    public async Task<TagDto?> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
    {
        var tag = await _tagUnitOfWork.Tags.GetByIdAsync(request.id,  cancellationToken, false);
        if (tag is null)
            return null;
        return new TagDto(tag.Id, tag.Name, tag.SuperTagId);
    }
}