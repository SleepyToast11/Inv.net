using Shared.Domain.Common;
using Shared.Domain.Common.Interfaces;
using Shared.Persistence.Entities.Tags;

namespace Shared.Domain.Tags;

public class Tag : IAggregateRoot
{
    private readonly List<ChangeSet> _changes = new();
    private readonly TagEntity _entity;

    public Tag(TagEntity entity)
    {
        _entity = entity ?? throw new ArgumentNullException(nameof(entity));
    }

    public IReadOnlyCollection<ChangeSet> Changes => _changes;
    public string Name => _entity.Name;
    public Guid SuperTagId => _entity.SuperTagId;
    public Guid TenantId => _entity.TenantId;

    public Guid Id => _entity.Id;

    public void Rename(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentNullException(nameof(newName));

        if (_entity.Name != newName)
        {
            _changes.Add(new ChangeSet("Tag.Rename", _entity.Name, newName, DateTime.UtcNow));
            _entity.Name = newName;
        }
    }
    
    public TagEntity ToEntity()
    {
        return _entity;
    }
}