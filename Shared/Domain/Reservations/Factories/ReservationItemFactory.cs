using Shared.Persistence.Entities.Reservation;

namespace Shared.Domain.Reservations.Factories;

public static class ReservationItemFactory
{
    public static ReservationItem Create(Guid reservationId, Guid itemId, Guid locationId, int quantity)
    {
        if (quantity < 0)
            throw new ArgumentException("Quantity cannot be negative", nameof(quantity));
        var entity = new ReservationItemEntity
        {
            ReservationId = reservationId,
            ItemId = itemId,
            LocationId = locationId,
            Quantity = quantity
        };
        return new ReservationItem(entity);
    }
}