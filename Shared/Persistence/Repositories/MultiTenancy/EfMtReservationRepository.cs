using Microsoft.EntityFrameworkCore;
using Shared.Domain.Reservations.Repositories;
using Shared.Persistence.Entities.Reservation;
using Shared.Persistence.Helpers;
using Shared.Persistence.Repositories.Common;
using Shared.Persistence.Repositories.Common.Generics;
using Shared.Persistence.Repositories.Common.Interfaces;
using Shared.Security;

namespace Shared.Persistence.Repositories.MultiTenancy;

public class EfMtReservationRepository(AppDbContext context, ICurrentTenantAccess tenantAccess) :
    GenericMultiTenantRepository<ReservationEntity>(context.Reservations, tenantAccess),
    IReservationRepository
{
    public async Task<IReadOnlyList<ReservationEntity>> GetWithDateFilterAsync(DateOnly fromDate, DateOnly toDate,
        CancellationToken cancellationToken, bool deepLoad = false)
    {
        return await ReadBaseQuery(deepLoad, r => r.FromDate >= fromDate && r.ToDate <= toDate)
            .ToListAsync(cancellationToken);
    }


    public async Task<IReadOnlyList<ReservationEntity>> GetReservationByUserIdAsync(Guid userId,
        CancellationToken cancellationToken, bool deepLoad = false)
    {
        return await ReadBaseQuery(deepLoad, r => r.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IPagedResult<ReservationEntity>> GetWithDateFilterPaginatedAsync(PageRequest pageRequest,
        DateOnly fromDate, DateOnly toDate,
        CancellationToken cancellationToken, bool deepLoad = false)
    {
        return await ReadBaseQuery(deepLoad, r => r.FromDate >= fromDate && r.ToDate <= toDate)
            .ToPagedResultAsync(pageRequest, cancellationToken);
    }

    public async Task<IPagedResult<ReservationEntity>> GetReservationByUserIdPaginatedAsync(PageRequest pageRequest,
        Guid userId, CancellationToken cancellationToken, bool deepLoad = false)
    {
        return await ReadBaseQuery(deepLoad, r => r.UserId == userId)
            .ToPagedResultAsync(pageRequest, cancellationToken);
    }

    //Come back here if there is a perf issue, as this can load quite a ton of data, so making a separate shallow repo might be good
    public override IQueryable<ReservationEntity> ApplyIncludes(IQueryable<ReservationEntity> query)
    {
        return query
            .Include(r => r.ReservationItems)
            .ThenInclude(ri => ri.Item)
            .ThenInclude(i => i.ItemLocations) //This allows to find out the amount of items and to verify against 
            .Include(r => r.ReservationItems)
            .ThenInclude(ri => ri.Location);
    }
}