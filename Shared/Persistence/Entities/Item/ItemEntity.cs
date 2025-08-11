using Shared.Persistence.Entities.Common.Interfaces;
using Shared.Persistence.Entities.Reservation;
using Shared.Persistence.Entities.Tags;
using Shared.Persistence.Entities.Tenant;

namespace Shared.Persistence.Entities.Item;

public class ItemEntity : IEntity, ITenantable
{
    public string Name { get; set; } = null!;

    public TenantEntity Tenant { get; set; } = null!;

    public List<ItemLocationEntity> ItemLocations { get; set; } = new();
    public List<TagItemEntity> TagItems { get; set; } = new();
    public List<ReservationItemEntity> ReservationItems { get; set; } = new();
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    
    
}