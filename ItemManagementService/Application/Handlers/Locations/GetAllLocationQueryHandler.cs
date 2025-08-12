using ItemManagementService.Application.Queries.Locations;
using ItemManagementService.Infrastructure.Dtos;
using ItemManagementService.Infrastructure.UnitOfWork;
using MediatR;
using Shared.Domain.Location;

namespace ItemManagementService.Application.Handlers.Locations;

public class GetAllLocationQueryHandler: IRequestHandler<GetAllLocationQuery, IReadOnlyCollection<LocationDto>>
{
    private readonly IItemUnitOfWork _unitOfWork;

    public GetAllLocationQueryHandler(IItemUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;


    public async Task<IReadOnlyCollection<LocationDto>> Handle(GetAllLocationQuery request, CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.Locations.GetAllAsync(cancellationToken, false);
        
        return entities.Select(e => new LocationDto(new Location(e))).ToList();
    }
}