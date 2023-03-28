namespace RoleplayReady.Domain.Models;

public record Entry
{
    public required EntryType Type { get; init; }
    public required string Title { get; init; }
    public required string Text { get; init; }

    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
    public bool IsDeleted { get; init; }
}