using ItemManagementService.Infrastructure.Dtos;
using MediatR;

namespace ItemManagementService.Application.Commands.Locations;

public record DeleteLocationCommand(Guid Id) : IRequest<bool>;