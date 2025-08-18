using MediatR;
using Shared.Domain.ApplicationUser;
using UserManagementService.Application.Query;
using UserManagementService.Infrastructure.Dto;
using UserManagementService.Infrastructure.UnitOfWork;

namespace UserManagementService.Api.Handlers;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserApplicationDto?>
{
    public IUserManagementUnitOfWork  UnitOfWork { get; }

    public GetUserByIdQueryHandler(IUserManagementUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    public async Task<UserApplicationDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await UnitOfWork.Users.GetByIdAsync(request.Id, cancellationToken, true);
        if  (user == null)
            return null;
        return new UserApplicationDto(new ApplicationUser(user));
    }
}