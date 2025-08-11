using Shared.Persistence.Entities.Item;

namespace Shared.Domain.Item;

public class ItemLocation
{
    private readonly ItemLocationEntity _entity;

    public ItemLocation(ItemLocationEntity entity)
    {
        _entity = entity ?? throw new ArgumentNullException(nameof(entity));
    }

    public Guid ItemId => _entity.ItemId;
    public Guid LocationId => _entity.LocationId;
    public int Quantity => _entity.Quantity;
    public DateTime CreatedAt => _entity.CreatedAt;


    public void UpdateQuantity(int newQuantity)
    {
        if (newQuantity < 0)
            throw new ArgumentOutOfRangeException(nameof(newQuantity));

        if (_entity.Quantity != newQuantity) _entity.Quantity = newQuantity;
    }

    public ItemLocationEntity ToEntity()
    {
        return _entity;
    }
}