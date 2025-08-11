using Shared.Domain.Common;
using Shared.Persistence.Entities.Tags;

namespace Shared.Domain.Tags;

public class TagItem
{
    private readonly List<ChangeSet> _changes = new();
    private readonly TagItemEntity _entity;

    public TagItem(TagItemEntity entity)
    {
        _entity = entity ?? throw new ArgumentNullException(nameof(entity));
    }

    public Guid ItemId => _entity.ItemId;
    public Guid TagId => _entity.TagId;

    public IReadOnlyCollection<ChangeSet> Changes => _changes;

    public TagItemEntity ToEntity()
    {
        return _entity;
    }
}