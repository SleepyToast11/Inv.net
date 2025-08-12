using ItemManagementService.Application.Commands.Items;
using ItemManagementService.Infrastructure.UnitOfWork;
using MediatR;
using Shared.Domain.Item;

namespace ItemManagementService.Application.Handlers.Items;

public class UpdateItemNameCommandHandler : IRequestHandler<UpdateItemNameCommand, bool>
{
    private readonly IItemUnitOfWork _unitOfWork;

    public UpdateItemNameCommandHandler(IItemUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UpdateItemNameCommand request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Items.UpdateAsync(request.ItemId, itemEntity =>
        {
            var item = new Item(itemEntity);
            item.ChangeName(request.NewName);
        }, cancellationToken);
    }
}
