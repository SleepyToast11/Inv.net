using Shared.Domain.Common;
using Shared.Persistence.Entities.Tags;

namespace Shared.Domain.Tags;

public class SuperTag
{
    private readonly List<ChangeSet> _changes = new();
    private readonly SuperTagEntity _entity;

    public SuperTag(SuperTagEntity entity)
    {
        _entity = entity;
    }

    public IReadOnlyCollection<ChangeSet> Changes => _changes;

    public Guid Id => _entity.Id;
    public string Name => _entity.Name;
    public Guid TenantId => _entity.TenantId;

    public void Rename(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentNullException(nameof(newName));

        if (_entity.Name != newName)
        {
            _changes.Add(new ChangeSet("SuperTag.Rename", _entity.Name, newName, DateTime.UtcNow));
            _entity.Name = newName ?? throw new ArgumentNullException(nameof(newName));
        }
    }
    public SuperTagEntity ToEntity() => _entity;
}