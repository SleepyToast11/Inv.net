using MediatR;
using Shared.Domain.ApplicationUser;
using UserManagementService.Application.Command;
using UserManagementService.Infrastructure.Dto;
using UserManagementService.Infrastructure.UnitOfWork;

namespace UserManagementService.Api.Handlers;

public class UpdateUserPermissionScopeCommandHandler : IRequestHandler<UpdateUserPermissionScopeCommand, Guid?>
{
    public IUserManagementUnitOfWork  UnitOfWork { get; }

    public UpdateUserPermissionScopeCommandHandler(IUserManagementUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    public async Task<Guid?> Handle(UpdateUserPermissionScopeCommand request, CancellationToken cancellationToken)
    {
        var updated = await UnitOfWork.Users.UpdateAsync(request.UserId, entity =>
        {
            var user = new ApplicationUser(entity);
            var permission = user.GetTenantPermission(request.TenantId);
            if (permission == null)
                throw new Exception("Permission tenant not found");
            permission.UpdatePermission(request.Scope, request.Level);
        }, cancellationToken);
        
        if (!updated)
            return null;
        
        return request.UserId;

    }
}