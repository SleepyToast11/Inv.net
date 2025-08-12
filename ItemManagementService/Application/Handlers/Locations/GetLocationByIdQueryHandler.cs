using ItemManagementService.Application.Queries.Locations;
using ItemManagementService.Infrastructure.Dtos;
using ItemManagementService.Infrastructure.UnitOfWork;
using MediatR;
using Shared.Domain.Location;

namespace ItemManagementService.Application.Handlers.Locations;

public class GetLocationByIdQueryHandler: IRequestHandler<GetLocationByIdQuery, LocationDto?>
{
    private readonly IItemUnitOfWork _unitOfWork;

    public GetLocationByIdQueryHandler(IItemUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<LocationDto?> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Locations.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null)
            return null;
        var location = new Location(entity);
        return new LocationDto(location);
    }
}