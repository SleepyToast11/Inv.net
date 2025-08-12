using MediatR;
using TagManagementService.Infrastructure.Dto;

namespace TagManagementService.Application.Command;

public record CreateSuperTagCommand(string Name, Guid TenantId): IRequest<Guid>;