using ItemManagementService.Application.Commands.Locations;
using ItemManagementService.Infrastructure.Dtos;
using ItemManagementService.Infrastructure.UnitOfWork;
using MediatR;
using Shared.Domain.Location;

namespace ItemManagementService.Application.Handlers.Locations;

public class RenameLocationCommandHandler: IRequestHandler<RenameLocationCommand, bool>
{
    private readonly IItemUnitOfWork _unitOfWork;

    public RenameLocationCommandHandler(IItemUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<bool> Handle(RenameLocationCommand request, CancellationToken cancellationToken)
    {
         return await _unitOfWork.Locations.UpdateAsync(request.Id, entity =>
        {
            var location = new Location(entity);
            location.Rename(request.NewName);
        }, cancellationToken);
    }
}