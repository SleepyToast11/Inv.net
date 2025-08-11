using MediatR;

namespace ItemManagementService.Application.Commands;

public record CreateItemCommand(string Name, Guid TenantId) : IRequest<Guid>;
