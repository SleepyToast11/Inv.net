using Shared.Domain.Common;
using Shared.Domain.Common.Interfaces;
using Shared.Domain.Reservations.Factories;
using Shared.Persistence.Entities.Reservation;

namespace Shared.Domain.Reservations;

public class Reservation : IAggregateRoot
{
    private readonly List<ChangeSet> _changes = new();
    private readonly ReservationEntity _entity;

    public Reservation(ReservationEntity entity)
    {
        _entity = entity;
    }

    public IReadOnlyCollection<ChangeSet> Changes => _changes;
    public Guid TenantId => _entity.TenantId;
    public Guid UserId => _entity.UserId;
    public DateOnly FromDate => _entity.FromDate;
    public DateOnly ToDate => _entity.ToDate;

    public IReadOnlyCollection<ReservationItemEntity> ReservationItems => _entity.ReservationItems.AsReadOnly();

    public Guid Id => _entity.Id;

    public void AddItem(Guid itemId, Guid locationId, int quantity)
    {
        if (quantity < 0)
            throw new ArgumentOutOfRangeException(nameof(quantity));

        var reservationItem = ReservationItemFactory.Create(Id, itemId, locationId, quantity);
        _entity.ReservationItems.Add(reservationItem.ToEntity());
        _changes.Add(new ChangeSet("ReservationItems.Add", null,
            $"Added location: {locationId}, item: {itemId}, Qty: {quantity}", DateTime.UtcNow));
    }

    public void RemoveItem(Guid itemId, Guid locationId)
    {
        var existing = _entity.ReservationItems
            .FirstOrDefault(ri => ri.ItemId == itemId && ri.LocationId == locationId);

        if (existing != null)
        {
            _entity.ReservationItems.Remove(existing);
            _changes.Add(new ChangeSet("ReservationItems.Remove", null, $"Removed: {locationId}, item: {itemId}",
                DateTime.UtcNow));
        }
    }

    public void UpdateItemQuantity(Guid itemId, Guid locationId, int newQuantity)
    {
        var existing = _entity.ReservationItems
                           .FirstOrDefault(ri => ri.ItemId == itemId && ri.LocationId == locationId)
                       ?? throw new InvalidOperationException("Reservation item not found.");
        var reservationItem = new ReservationItem(existing);

        if (newQuantity <= 0)
        {
            RemoveItem(itemId, locationId);
        }
        else
        {
            _changes.Add(new ChangeSet("ReservationItems.Update", reservationItem.Quantity.ToString(),
                $"updated location: {locationId}, item: {itemId}, Qty: {newQuantity}", DateTime.UtcNow));
            reservationItem.SetQuantity(newQuantity);
        }
    }
}