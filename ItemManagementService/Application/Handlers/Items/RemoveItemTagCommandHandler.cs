using ItemManagementService.Application.Commands.Items;
using ItemManagementService.Infrastructure.UnitOfWork;
using MediatR;
using Shared.Domain.Item;

namespace ItemManagementService.Application.Handlers.Items;

public class RemoveItemTagCommandHandler : IRequestHandler<RemoveItemTagCommand, bool>
{
    private readonly IItemUnitOfWork _unitOfWork;

    public RemoveItemTagCommandHandler(IItemUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<bool> Handle(RemoveItemTagCommand request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Items.UpdateAsync(request.ItemId, itemEntity =>
        {
            var item = new Item(itemEntity);
            item.RemoveTag(request.TagId);
        }, cancellationToken);
    }
}
