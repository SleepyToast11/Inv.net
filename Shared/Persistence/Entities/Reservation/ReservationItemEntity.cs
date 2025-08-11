using Shared.Persistence.Entities.Item;
using Shared.Persistence.Entities.Location;

namespace Shared.Persistence.Entities.Reservation;

public class ReservationItemEntity
{
    public ReservationEntity Reservation { get; set; } = null!;
    public Guid ReservationId { get; set; }

    public ItemEntity Item { get; set; } = null!;
    public Guid ItemId { get; set; }

    public LocationEntity Location { get; set; } = null!;
    public Guid LocationId { get; set; }

    public int Quantity { get; set; }
}