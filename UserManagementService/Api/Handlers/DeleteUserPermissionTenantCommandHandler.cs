using MediatR;
using Shared.Domain.ApplicationUser;
using UserManagementService.Application.Command;
using UserManagementService.Infrastructure.UnitOfWork;

namespace UserManagementService.Api.Handlers;

public class DeleteUserPermissionTenantCommandHandler : IRequestHandler<DeleteUserPermissionTenantCommand, bool>
{
    public IUserManagementUnitOfWork  UnitOfWork { get; }

    public DeleteUserPermissionTenantCommandHandler(IUserManagementUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeleteUserPermissionTenantCommand request, CancellationToken cancellationToken)
    {
        var updated = await UnitOfWork.Users.UpdateAsync(request.UserId, entity =>
        {
            var user = new ApplicationUser(entity);
            var deleted = user.RemoveTenantPermission(request.TenantId);
            if (!deleted)
                throw new Exception("Permission not found");
        }, cancellationToken);
        return updated;
    }
}