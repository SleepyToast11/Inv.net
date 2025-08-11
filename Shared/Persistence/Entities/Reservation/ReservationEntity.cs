using Shared.Persistence.Entities.Common.Interfaces;

namespace Shared.Persistence.Entities.Reservation;

public class ReservationEntity : IEntity, ITenantable
{
    public Guid UserId { get; set; }

    public DateOnly FromDate { get; set; }
    public DateOnly ToDate { get; set; }

    public List<ReservationItemEntity> ReservationItems { get; set; } = new();
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }
}