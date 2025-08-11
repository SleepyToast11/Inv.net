using Shared.Domain.Tags.Repositories;
using Shared.Persistence.Entities.Tags;
using Shared.Persistence.Repositories.Common.Generics;
using Shared.Security;

namespace Shared.Persistence.Repositories.MultiTenancy;

public class EfMtSuperTagRepository(AppDbContext context, ICurrentTenantAccess tenantAccess)
    : GenericMultiTenantRepository<SuperTagEntity>(context.SuperTags, tenantAccess), ISuperTagRepository;