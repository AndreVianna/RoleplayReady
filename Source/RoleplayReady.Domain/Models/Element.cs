namespace RoleplayReady.Domain.Models;

public record Element
{
    public required System System { get; init; }
    public required string OwnerId { get; init; }
    public required string Name { get; init; }
    // System, OwnerId, and Name must be unique.

    public required ElementType Type { get; init; }

    public string? Description { get; init; }

    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
    public Availability Availability { get; init; }

    public IList<IElementProperty> Properties { get; init; } = new List<IElementProperty>();
}