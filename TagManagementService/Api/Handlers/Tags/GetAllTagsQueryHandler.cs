using MediatR;
using Microsoft.AspNetCore.Mvc;
using TagManagementService.Application.Queries;
using TagManagementService.Infrastructure.Dto;
using TagManagementService.Infrastructure.UnitOfWork;

namespace TagManagementService.Api.Handlers.Tags;

public class GetAllTagsQueryHandler: IRequestHandler<GetAllTagsQuery, IReadOnlyList<TagDto>>
{
    private readonly ITagUnitOfWork _tagUnitOfWork;
    
    public GetAllTagsQueryHandler(ITagUnitOfWork tagUnitOfWork) => _tagUnitOfWork = tagUnitOfWork;

    public async Task<IReadOnlyList<TagDto>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
    {
        var tags = await _tagUnitOfWork.Tags.GetAllAsync(cancellationToken, false);
        return tags.Select(x => new TagDto(x.Id, x.Name, x.SuperTagId)).ToList();
    }
}