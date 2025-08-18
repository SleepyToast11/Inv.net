using MediatR;
using UserManagementService.Infrastructure.Dto;

namespace UserManagementService.Application.Command;

public record AddUserPermissionTenantCommand(Guid UserId, Guid TenantId): IRequest<Guid?>;