using Shared.Domain.Tags.Repositories;
using Shared.Persistence.Entities.Tags;
using Shared.Persistence.Repositories.Common.Generics;
using Shared.Security;

namespace Shared.Persistence.Repositories.MultiTenancy;

public class EfMtTagRepository(AppDbContext context, ICurrentTenantAccess tenantAccess) :
    GenericMultiTenantRepository<TagEntity>(context.Tags, tenantAccess), ITagRepository;