using MediatR;
using Shared.Persistence.Entities.ApplicationUser;
using UserManagementService.Infrastructure.Dto;

namespace UserManagementService.Application.Command;

public record UpdateUserPermissionScopeCommand(Guid UserId, Guid TenantId, string Scope, PermissionLevel Level): IRequest<Guid?>;