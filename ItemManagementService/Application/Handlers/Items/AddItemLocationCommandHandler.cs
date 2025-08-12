using ItemManagementService.Application.Commands.Items;
using ItemManagementService.Infrastructure.UnitOfWork;
using MediatR;
using Shared.Domain.Item;

namespace ItemManagementService.Application.Handlers.Items;

public class AddItemLocationCommandHandler : IRequestHandler<AddItemLocationCommand, bool>
{
    private readonly IItemUnitOfWork _unitOfWork;

    public AddItemLocationCommandHandler(IItemUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<bool> Handle(AddItemLocationCommand request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Items.UpdateAsync(request.ItemId, itemEntity =>
        {
            var item = new Item(itemEntity);
            item.AddLocation(request.LocationId, request.Quantity);
        }, cancellationToken);
    }
}

