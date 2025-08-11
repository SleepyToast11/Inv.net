using MediatR;

namespace TagManagementService.Application.Command;

public record CreateTagCommand(string Name, Guid TenantId, Guid SuperTagId):IRequest<Guid>;