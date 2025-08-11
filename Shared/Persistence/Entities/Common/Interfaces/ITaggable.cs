using Shared.Domain.Tags;

namespace Shared.Persistence.Entities.Common.Interfaces;

public interface ITaggable<TTagItem>
{
    IReadOnlyCollection<TagItem> TagItems { get; }

    void AddTag(Guid tagId);
    void RemoveTag(Guid tagId);
}