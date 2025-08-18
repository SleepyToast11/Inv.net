using MediatR;
using Shared.Domain.ApplicationUser;
using UserManagementService.Application.Command;
using UserManagementService.Infrastructure.UnitOfWork;

namespace UserManagementService.Api.Handlers;

public class DeleteUserPermissionScopeHandler : IRequestHandler<DeleteUserPermissionScope, bool>
{
    public IUserManagementUnitOfWork  UnitOfWork { get; }

    public DeleteUserPermissionScopeHandler(IUserManagementUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteUserPermissionScope request, CancellationToken cancellationToken)
    {
        var updated = await UnitOfWork.Users.UpdateAsync(request.userId, entity =>
        {
            var user = new ApplicationUser(entity);
            var permission = user.GetTenantPermission(request.TenantId);
            if (permission == null)
                throw new Exception("Permission not found");
            var deleted = permission.RemovePermission(request.Scope);
            if (!deleted)
                throw new Exception("Permission scope not found");
        }, cancellationToken);
        return updated;
    }
}