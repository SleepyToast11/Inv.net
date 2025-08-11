using Shared.Domain.Common.Interfaces;
using Shared.Persistence.Entities.Tenant;

namespace Shared.Domain.Tenant.Repositories;

public interface ITenantRepository : IGenericRepository<TenantEntity>
{
}