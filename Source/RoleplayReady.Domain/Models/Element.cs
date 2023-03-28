namespace RoleplayReady.Domain.Models;

public record Element
{
    // System, OwnerId, and Name must be unique.
    public required GameSystem System { get; init; }
    public required string OwnerId { get; init; }
    public required string Name { get; init; }

    public required ElementType Type { get; init; }
    public Usage Usage { get; init; }
    public required Source Source { get; init; }

    public string? Description { get; init; }

    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
    public SystemStatus SystemStatus { get; init; }

    public IList<IElementFeature> Features { get; init; } = new List<IElementFeature>();
}