using ItemManagementService.Infrastructure.Dtos;
using MediatR;

namespace ItemManagementService.Application.Queries.Locations;

public record GetAllLocationQuery(): IRequest<IReadOnlyCollection<LocationDto>>;