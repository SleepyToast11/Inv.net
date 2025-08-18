using MediatR;

namespace UserManagementService.Application.Command;

public record DeleteUserPermissionScope(Guid userId, Guid TenantId, string Scope): IRequest<bool>;