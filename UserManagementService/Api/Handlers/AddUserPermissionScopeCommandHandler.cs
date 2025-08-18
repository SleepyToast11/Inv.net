using MediatR;
using Shared.Domain.ApplicationUser;
using Shared.Persistence.Entities.ApplicationUser;
using UserManagementService.Application.Command;
using UserManagementService.Infrastructure.Dto;
using UserManagementService.Infrastructure.UnitOfWork;

namespace UserManagementService.Api.Handlers;

public class AddUserPermissionScopeCommandHandler : IRequestHandler<AddUserPermissionScopeCommand, Guid?>
{
    public IUserManagementUnitOfWork  UnitOfWork { get; }

    public AddUserPermissionScopeCommandHandler(IUserManagementUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }
    
    public async Task<Guid?> Handle(AddUserPermissionScopeCommand request, CancellationToken cancellationToken)
    {
        var updated = await UnitOfWork.Users.UpdateAsync(request.UserId, entity =>
        {
            var user = new ApplicationUser(entity);
            var permission = user.GetTenantPermission(request.TenantId);
            if (permission == null)
                throw new Exception("Permission tenant not found");
            permission.AddPermission(request.Scope, request.PermissionLevel);
        }, cancellationToken);

        if (!updated)
            return null;
        
        return request.UserId;
    }
}