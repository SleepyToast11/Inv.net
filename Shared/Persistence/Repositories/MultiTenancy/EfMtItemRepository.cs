using Microsoft.EntityFrameworkCore;
using Shared.Domain.Item.Repositories;
using Shared.Persistence.Entities.Item;
using Shared.Persistence.Repositories.Common.Generics;
using Shared.Security;
using Shared.Persistence.Entities.Common.Extensions;

namespace Shared.Persistence.Repositories.MultiTenancy;

public class EfMtItemRepository(AppDbContext context, ICurrentTenantAccess tenantAccess)
    : GenericMultiTenantRepository<ItemEntity>(context.Items, tenantAccess), IItemRepository
{
    public override IQueryable<ItemEntity> ApplyIncludes(IQueryable<ItemEntity> query)
    {
        return query
            .Include(i => i.ItemLocations)
            .ThenInclude(il => il.Location)
            .Include(i => i.TagItems)
            .ThenInclude(it => it.Tag)
            .Include(i => i.ReservationItems)
            .ThenInclude(il => il.Reservation);
    }
}