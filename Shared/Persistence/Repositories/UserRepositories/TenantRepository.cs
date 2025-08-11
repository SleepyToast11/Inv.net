using Shared.Domain.Tenant.Repositories;
using Shared.Persistence.Entities.Tenant;
using Shared.Persistence.Repositories.Common.Generics;

namespace Shared.Persistence.Repositories.UserRepositories;

public class TenantRepository(AppDbContext context)
    : GenericRepository<TenantEntity>(context.Tenants), ITenantRepository;