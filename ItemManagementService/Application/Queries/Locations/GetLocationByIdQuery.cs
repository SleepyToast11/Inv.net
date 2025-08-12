using ItemManagementService.Infrastructure.Dtos;
using MediatR;

namespace ItemManagementService.Application.Queries.Locations;

public record GetLocationByIdQuery(Guid Id): IRequest<LocationDto?>;