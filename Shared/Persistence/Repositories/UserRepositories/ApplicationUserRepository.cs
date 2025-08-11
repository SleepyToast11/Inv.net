using Microsoft.EntityFrameworkCore;
using Shared.Domain.ApplicationUser.Repositories;
using Shared.Persistence.Entities.ApplicationUser;
using Shared.Persistence.Repositories.Common.Generics;

namespace Shared.Persistence.Repositories.UserRepositories;

public class ApplicationUserRepository(AppDbContext context)
    : GenericRepository<ApplicationUserEntity>(context.Users), IApplicationUserRepository
{
    public override IQueryable<ApplicationUserEntity> ApplyIncludes(IQueryable<ApplicationUserEntity> query)
    {
        return query
            .Include(au => au.UserPermissionEntities)
            .ThenInclude(up => up.Permissions);
    }
}