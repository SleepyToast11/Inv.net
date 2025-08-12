using ItemManagementService.Infrastructure.Dtos;
using MediatR;

namespace ItemManagementService.Application.Commands.Locations;

public record CreateLocationCommand(string Name, Guid TenantId) : IRequest<Guid>;