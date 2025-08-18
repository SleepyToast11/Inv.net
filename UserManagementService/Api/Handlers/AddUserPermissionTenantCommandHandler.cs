using MediatR;
using Shared.Domain.ApplicationUser;
using UserManagementService.Application.Command;
using UserManagementService.Infrastructure.Dto;
using UserManagementService.Infrastructure.UnitOfWork;

namespace UserManagementService.Api.Handlers;

public class AddUserPermissionTenantCommandHandler : IRequestHandler<AddUserPermissionTenantCommand, Guid?>
{
    public IUserManagementUnitOfWork  UnitOfWork { get; }

    public AddUserPermissionTenantCommandHandler(IUserManagementUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    public async Task<Guid?> Handle(AddUserPermissionTenantCommand request, CancellationToken cancellationToken)
    {
        var updated = await UnitOfWork.Users.UpdateAsync(request.UserId, entity =>
        {
            var user = new ApplicationUser(entity);
            user.CreateTenantPermission(request.TenantId);
        }, cancellationToken);
        
        if (!updated)
            return null;
        
        return request.UserId;
    }
}