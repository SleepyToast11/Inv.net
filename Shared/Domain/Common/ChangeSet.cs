namespace Shared.Domain.Common;

public record ChangeSet(string PropertyName, string? OldValue, string? NewValue, DateTime Timestamp);