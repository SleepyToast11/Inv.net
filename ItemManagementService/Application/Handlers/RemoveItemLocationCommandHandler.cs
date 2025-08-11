using ItemManagementService.Application.Commands;
using ItemManagementService.Infrastructure.UnitOfWork;
using MediatR;
using Shared.Domain.Item;

namespace ItemManagementService.Application.Handlers;

public class RemoveItemLocationCommandHandler : IRequestHandler<RemoveItemLocationCommand, bool>
{
    private readonly IItemUnitOfWork _unitOfWork;

    public RemoveItemLocationCommandHandler(IItemUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<bool> Handle(RemoveItemLocationCommand request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Items.UpdateAsync(request.ItemId, itemEntity =>
        {
            var item = new Item(itemEntity);
            item.RemoveLocation(request.LocationId);
        }, cancellationToken);
    }
}
