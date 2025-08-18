using Shared.Domain.ApplicationUser.Repositories;
using Shared.Persistence;
using Shared.Persistence.Repositories.Common;

namespace UserManagementService.Infrastructure.UnitOfWork;

public class UserManagementUnitOfWork(AppDbContext context, IApplicationUserRepository iApplicationUserRepository) : BaseUnitOfWork(context), IUserManagementUnitOfWork
{
    public IApplicationUserRepository Users { get; } = iApplicationUserRepository;
}