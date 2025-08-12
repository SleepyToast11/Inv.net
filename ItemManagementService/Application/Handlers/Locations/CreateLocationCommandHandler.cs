using ItemManagementService.Application.Commands.Locations;
using ItemManagementService.Infrastructure.UnitOfWork;
using MediatR;
using Shared.Domain.Location;

namespace ItemManagementService.Application.Handlers.Locations;

public class CreateLocationCommandHandler: IRequestHandler<CreateLocationCommand, Guid>
{
    private readonly IItemUnitOfWork _unitOfWork;

    public CreateLocationCommandHandler(IItemUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Guid> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
    {
        var location = LocationFactory.Create(request.Name, request.TenantId);
        await _unitOfWork.Locations.AddAsync(location.ToEntity(), cancellationToken);
        return location.Id;
    }
}