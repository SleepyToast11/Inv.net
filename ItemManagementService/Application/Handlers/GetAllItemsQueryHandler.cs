using ItemManagementService.Application.Queries;
using ItemManagementService.Infrastructure.Dtos;
using ItemManagementService.Infrastructure.UnitOfWork;
using MediatR;

namespace ItemManagementService.Application.Handlers;

public class GetAllItemsQueryHandler : IRequestHandler<GetAllItemsQuery, IReadOnlyList<ItemDto>>
{
    private readonly IItemUnitOfWork _unitOfWork;

    public GetAllItemsQueryHandler(IItemUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<ItemDto>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.Items.GetAllAsync(cancellationToken, false);

        return entities.Select(e => new ItemDto(e.Id, e.Name, e.TenantId)).ToList();
    }
}
