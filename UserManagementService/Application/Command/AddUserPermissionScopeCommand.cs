using System.Windows.Input;
using MediatR;
using Shared.Persistence.Entities.ApplicationUser;
using UserManagementService.Infrastructure.Dto;

namespace UserManagementService.Application.Command;

public record AddUserPermissionScopeCommand(Guid UserId, Guid TenantId, string Scope, PermissionLevel PermissionLevel)
    : IRequest<Guid?>;