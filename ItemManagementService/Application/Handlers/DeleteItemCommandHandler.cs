using ItemManagementService.Application.Commands;
using ItemManagementService.Infrastructure.UnitOfWork;
using MediatR;

namespace ItemManagementService.Application.Handlers;

public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, bool>
{
    private readonly IItemUnitOfWork _unitOfWork;

    public DeleteItemCommandHandler(IItemUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Items.DeleteAsync(request.ItemId, cancellationToken);
    }
}

