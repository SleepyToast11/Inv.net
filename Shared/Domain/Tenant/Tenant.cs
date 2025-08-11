using Shared.Domain.Common;
using Shared.Persistence.Entities.Tenant;

namespace Shared.Domain.Tenant;

public class Tenant
{
    private readonly List<ChangeSet> _changes = new();
    private readonly TenantEntity _entity;

    public Tenant(TenantEntity entity)
    {
        _entity = entity;
    }

    public IReadOnlyCollection<ChangeSet> Changes => _changes;

    public Guid Id => _entity.Id;
    public string Name => _entity.Name;

    public void Rename(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentNullException(nameof(newName));

        if (_entity.Name != newName)
        {
            _entity.Name = newName ?? throw new ArgumentNullException(nameof(newName));
            _changes.Add(new ChangeSet("Tenant.Rename", _entity.Name, newName, DateTime.UtcNow));
        }
    }
}