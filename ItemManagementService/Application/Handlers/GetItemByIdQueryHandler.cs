using ItemManagementService.Application.Queries;
using ItemManagementService.Infrastructure.Dtos;
using ItemManagementService.Infrastructure.UnitOfWork;
using MediatR;

namespace ItemManagementService.Application.Handlers;

public class GetItemByIdQueryHandler : IRequestHandler<GetItemByIdQuery, ItemDto?>
{
    private readonly IItemUnitOfWork _unitOfWork;

    public GetItemByIdQueryHandler(IItemUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ItemDto?> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Items.GetByIdAsync(request.ItemId, cancellationToken, false);

        if (entity == null)
            return null;

        return new ItemDto(entity.Id, entity.Name, entity.TenantId);
    }
}
