using ItemManagementService.Application.Commands.Locations;
using ItemManagementService.Infrastructure.UnitOfWork;
using MediatR;

namespace ItemManagementService.Application.Handlers.Locations;

public class DeleteLocationCommandHandler: IRequestHandler<DeleteLocationCommand, bool>
{
    private readonly IItemUnitOfWork _unitOfWork;

    public DeleteLocationCommandHandler(IItemUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<bool> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Locations.DeleteAsync(request.Id, cancellationToken);
    }
}