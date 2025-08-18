using MediatR;
using Shared.Persistence.Entities.ApplicationUser;
using UserManagementService.Infrastructure.Dto;

namespace UserManagementService.Application.Command;

public record DeleteUserPermissionTenantCommand(Guid UserId, Guid TenantId): IRequest<bool>;