using ItemManagementService.Application.Commands;
using ItemManagementService.Infrastructure.UnitOfWork;
using MediatR;
using Shared.Domain.Item;

namespace ItemManagementService.Application.Handlers;

public class UpdateItemLocationQuantityCommandHandler : IRequestHandler<UpdateItemLocationQuantityCommand, bool>
{
    private readonly IItemUnitOfWork _unitOfWork;

    public UpdateItemLocationQuantityCommandHandler(IItemUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<bool> Handle(UpdateItemLocationQuantityCommand request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Items.UpdateAsync(request.ItemId, itemEntity =>
        {
            var item = new Item(itemEntity);
            item.UpdateLocationQuantity(request.LocationId, request.NewQuantity);
        }, cancellationToken);
    }
}

