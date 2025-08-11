using Shared.Persistence.Entities.Reservation;

namespace Shared.Domain.Reservations.Factories;

public static class ReservationFactory
{
    public static Reservation Create(Guid tenantId, Guid userId, DateOnly fromDate, DateOnly toDate)
    {
        if (toDate < fromDate)
            throw new ArgumentException("ToDate must be after FromDate.");

        var entity = new ReservationEntity
        {
            Id = Guid.NewGuid(),
            TenantId = tenantId,
            UserId = userId,
            FromDate = fromDate,
            ToDate = toDate
        };
        return new Reservation(entity);
    }
}