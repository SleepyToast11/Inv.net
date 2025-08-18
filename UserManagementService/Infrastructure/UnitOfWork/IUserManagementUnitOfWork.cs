using Shared.Domain.ApplicationUser.Repositories;
using Shared.Persistence.Repositories.Common.Interfaces;

namespace UserManagementService.Infrastructure.UnitOfWork;

public interface IUserManagementUnitOfWork: IUnitOfWork
{
    public IApplicationUserRepository Users { get; }
}