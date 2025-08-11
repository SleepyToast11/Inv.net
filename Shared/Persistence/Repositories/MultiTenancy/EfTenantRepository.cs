using System.Linq.Expressions;
using Shared.Persistence.Entities.Tenant;
using Shared.Persistence.Repositories.Common.Generics;

namespace Shared.Persistence.Repositories.MultiTenancy;

public class EfTenantRepository(
    AppDbContext context,
    Expression<Func<TenantEntity, bool>>? whereFilter = null)
    : GenericRepository<TenantEntity>(context.Tenants, whereFilter);