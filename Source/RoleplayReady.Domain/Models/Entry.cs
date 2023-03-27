namespace RoleplayReady.Domain.Models;

public record Entry
{
    public required string Title { get; init; }
    public required string Content { get; init; }

    public required DateTime Timestamp { get; init; } // Key
    public bool IsDeleted { get; init; }
}