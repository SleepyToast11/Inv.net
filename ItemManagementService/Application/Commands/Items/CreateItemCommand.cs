using MediatR;

namespace ItemManagementService.Application.Commands.Items;

public record CreateItemCommand(string Name, Guid TenantId) : IRequest<Guid>;
