using Shared.Domain.Location.Repositories;
using Shared.Persistence.Entities.Location;
using Shared.Persistence.Repositories.Common.Generics;
using Shared.Security;

namespace Shared.Persistence.Repositories.MultiTenancy;

public class EfMtLocationRepository(AppDbContext context, ICurrentTenantAccess tenantAccess) :
    GenericMultiTenantRepository<LocationEntity>(context.Locations, tenantAccess), ILocationRepository;