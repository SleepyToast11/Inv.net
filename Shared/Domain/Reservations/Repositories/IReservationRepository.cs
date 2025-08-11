using Shared.Domain.Common.Interfaces;
using Shared.Persistence.Entities.Reservation;
using Shared.Persistence.Repositories.Common;
using Shared.Persistence.Repositories.Common.Interfaces;

namespace Shared.Domain.Reservations.Repositories;

public interface IReservationRepository : IGenericRepository<ReservationEntity>
{
    Task<IReadOnlyList<ReservationEntity>> GetWithDateFilterAsync(DateOnly fromDate, DateOnly toDate,
        CancellationToken cancellationToken, bool deepLoad = false);

    Task<IReadOnlyList<ReservationEntity>> GetReservationByUserIdAsync(Guid userId, CancellationToken cancellationToken,
        bool deepLoad = false);

    Task<IPagedResult<ReservationEntity>> GetWithDateFilterPaginatedAsync(PageRequest pageRequest,
        DateOnly fromDate, DateOnly toDate,
        CancellationToken cancellationToken, bool deepLoad = false);

    Task<IPagedResult<ReservationEntity>> GetReservationByUserIdPaginatedAsync(PageRequest pageRequest, Guid userId,
        CancellationToken cancellationToken, bool deepLoad = false);
}