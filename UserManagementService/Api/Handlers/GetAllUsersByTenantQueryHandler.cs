using MediatR;
using Shared.Domain.ApplicationUser;
using UserManagementService.Application.Query;
using UserManagementService.Infrastructure.Dto;
using UserManagementService.Infrastructure.UnitOfWork;

namespace UserManagementService.Api.Handlers;

public class GetAllUsersByTenantQueryHandler : IRequestHandler<GetAllUsersByTenantQuery, IReadOnlyList<UserApplicationDto>>
{
    public IUserManagementUnitOfWork  UnitOfWork { get; }

    public GetAllUsersByTenantQueryHandler(IUserManagementUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<UserApplicationDto>> Handle(GetAllUsersByTenantQuery request, CancellationToken cancellationToken)
    {
        var users = await UnitOfWork.Users.GetAllUsersByTenant(request.TenantId);
        return users.Select(x => new UserApplicationDto(new ApplicationUser(x))).ToList();
    }
}