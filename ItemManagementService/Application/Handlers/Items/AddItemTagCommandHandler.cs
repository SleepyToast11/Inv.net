using ItemManagementService.Application.Commands.Items;
using ItemManagementService.Infrastructure.UnitOfWork;
using MediatR;
using Shared.Domain.Item;

namespace ItemManagementService.Application.Handlers.Items;

public class AddItemTagCommandHandler : IRequestHandler<AddItemTagCommand, bool>
{
    private readonly IItemUnitOfWork _unitOfWork;

    public AddItemTagCommandHandler(IItemUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<bool> Handle(AddItemTagCommand request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Items.UpdateAsync(request.ItemId, itemEntity =>
        {
            var item = new Item(itemEntity);
            item.AddTag(request.TagId);
        }, cancellationToken);
    }
}
