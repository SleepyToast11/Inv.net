using Shared.Persistence.Entities.Reservation;

namespace Shared.Domain.Reservations;

public class ReservationItem
{
    private readonly ReservationItemEntity _entity;

    public ReservationItem(ReservationItemEntity entity)
    {
        _entity = entity;
    }

    public Guid ReservationId => _entity.ReservationId;

    public Guid ItemId => _entity.ItemId;
    public Guid LocationId => _entity.LocationId;
    public int Quantity => _entity.Quantity;

    public void SetQuantity(int quantity)
    {
        if (quantity < 0)
            throw new ArgumentException("Quantity cannot be negative", nameof(quantity));

        _entity.Quantity = quantity;
    }

    public ReservationItemEntity ToEntity()
    {
        return _entity;
    }
}