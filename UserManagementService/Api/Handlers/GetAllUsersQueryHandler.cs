using MediatR;
using Shared.Domain.ApplicationUser;
using UserManagementService.Application.Query;
using UserManagementService.Infrastructure.Dto;
using UserManagementService.Infrastructure.UnitOfWork;

namespace UserManagementService.Api.Handlers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IReadOnlyList<UserApplicationDto>>
{
    public IUserManagementUnitOfWork  UnitOfWork { get; }

    public GetAllUsersQueryHandler(IUserManagementUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<UserApplicationDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await UnitOfWork.Users.GetAllAsync(cancellationToken, false);
        return users.Select(x => new UserApplicationDto(new ApplicationUser(x))).ToList();
    }
}