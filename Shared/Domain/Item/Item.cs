using Shared.Domain.Common;
using Shared.Domain.Common.Interfaces;
using Shared.Domain.Tags;
using Shared.Persistence.Entities.Common.Interfaces;
using Shared.Persistence.Entities.Item;
using Shared.Persistence.Entities.Reservation;

namespace Shared.Domain.Item;

public class Item : IAggregateRoot, ITaggable<TagItem>
{
    private readonly List<ChangeSet> _changes = new();
    private readonly ItemEntity _entity;

    private readonly List<TagItem> _tagItems;

    public Item(ItemEntity entity)
    {
        _entity = entity ?? throw new ArgumentNullException(nameof(entity));
        _tagItems = _entity.TagItems.Select(t => new TagItem(t)).ToList();
    }

    public IReadOnlyCollection<ChangeSet> Changes => _changes;
    public string Name => _entity.Name;
    public Guid TenantId => _entity.TenantId;

    public IReadOnlyCollection<ItemLocationEntity> ItemLocations => _entity.ItemLocations.AsReadOnly();

    public IReadOnlyCollection<ReservationItemEntity> ReservationItems => _entity.ReservationItems.AsReadOnly();

    public Guid Id => _entity.Id;
    public IReadOnlyCollection<TagItem> TagItems => _tagItems.AsReadOnly();


    public void AddTag(Guid tagId)
    {
        if (_tagItems.Any(t => t.TagId == tagId))
            throw new InvalidOperationException("Tag already exists.");

        var tagItem = TagItemFactory.Create(_entity.Id, tagId);
        _tagItems.Add(tagItem);

        _entity.TagItems.Add(tagItem.ToEntity());

        _changes.Add(new ChangeSet("TagItem.Add", null, tagId.ToString(), DateTime.UtcNow));
    }

    public void RemoveTag(Guid tagId)
    {
        var existing = _tagItems.FirstOrDefault(t => t.TagId == tagId)
                       ?? throw new InvalidOperationException("Tag not found.");

        _tagItems.Remove(existing);
        _entity.TagItems.Remove(existing.ToEntity());

        _changes.Add(new ChangeSet("TagItem.Remove", tagId.ToString(), null, DateTime.UtcNow));
    }

    public void ChangeName(string newName)
    {
        if (_entity.Name != newName)
        {
            _changes.Add(new ChangeSet(nameof(_entity.Name), _entity.Name, newName, DateTime.UtcNow));
            _entity.Name = newName;
        }
    }

    public void AddLocation(Guid locationId, int quantity)
    {
        if (_entity.ItemLocations.Any(l => l.LocationId == locationId))
            throw new InvalidOperationException("Location already exists.");

        var newItemLocation = ItemLocationFactory.Create(_entity.Id, locationId, quantity);
        _entity.ItemLocations.Add(newItemLocation.ToEntity());
        _changes.Add(new ChangeSet("ItemLocation", null, $"Added: {locationId}, Qty: {quantity}", DateTime.UtcNow));
    }

    public void UpdateLocationQuantity(Guid locationId, int newQuantity)
    {
        var locEntity = _entity.ItemLocations.FirstOrDefault(l => l.LocationId == locationId)
                        ?? throw new InvalidOperationException("Location not found.");
        var loc = new ItemLocation(locEntity);
        _changes.Add(new ChangeSet("ItemLocation.Quantity", loc.Quantity.ToString(), newQuantity.ToString(),
            DateTime.UtcNow));
        loc.UpdateQuantity(newQuantity);
    }

    public void RemoveLocation(Guid locationId)
    {
        var loc = _entity.ItemLocations.FirstOrDefault(l => l.LocationId == locationId);
        if (loc != null)
        {
            _entity.ItemLocations.Remove(loc);
            _changes.Add(new ChangeSet("ItemLocation", $"Removed: {locationId}", null, DateTime.UtcNow));
        }
    }

    public ItemEntity ToEntity()
    {
        return _entity;
    }
}