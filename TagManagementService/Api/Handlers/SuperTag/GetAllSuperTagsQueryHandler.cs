using MediatR;
using TagManagementService.Application.Queries;
using TagManagementService.Infrastructure.Dto;
using TagManagementService.Infrastructure.UnitOfWork;

namespace TagManagementService.Api.Handlers.SuperTag;

public class GetAllSuperTagsQueryHandler: IRequestHandler<GetAllSuperTagsQuery, IReadOnlyList<SuperTagDto>>
{
    
    private readonly ITagUnitOfWork _tagUnitOfWork;
    
    public GetAllSuperTagsQueryHandler(ITagUnitOfWork tagUnitOfWork) => _tagUnitOfWork = tagUnitOfWork;

    public async Task<IReadOnlyList<SuperTagDto>> Handle(GetAllSuperTagsQuery request, CancellationToken cancellationToken)
    {
        var superTags = await _tagUnitOfWork.SuperTags.GetAllAsync(cancellationToken, false);
        return superTags.Select(x => new SuperTagDto(x.Id, x.Name)).ToList();
    }
}