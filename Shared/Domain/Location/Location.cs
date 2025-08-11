using Shared.Domain.Common;
using Shared.Domain.Common.Interfaces;
using Shared.Persistence.Entities.Location;

namespace Shared.Domain.Location;

public class Location : IAggregateRoot
{
    private readonly List<ChangeSet> _changes = new();

    private readonly LocationEntity _entity = new();

    public Location(LocationEntity entity)
    {
        _entity = entity ?? throw new ArgumentNullException(nameof(entity));
    }

    public IReadOnlyCollection<ChangeSet> Changes => _changes;
    public string Name => _entity.Name;
    public Guid TenantId => _entity.TenantId;

    public Guid Id => _entity.Id;


    public void Rename(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentNullException(nameof(newName));

        if (_entity.Name != newName)
        {
            _entity.Name = newName ?? throw new ArgumentNullException(nameof(newName));
            _changes.Add(new ChangeSet("Location.Rename", Name, newName, DateTime.UtcNow));
        }
    }

    public LocationEntity ToEntity()
    {
        return _entity;
    }
}