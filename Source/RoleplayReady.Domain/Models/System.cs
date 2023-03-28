namespace RoleplayReady.Domain.Models;

public record System
{
    // Name must be unique.
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string Abbreviation { get; init; }
    public required string Publisher { get; init; }

    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
    public Availability Availability { get; init; }

    public IList<ElementType> ElementTypes { get; init; } = new List<ElementType>();
    public IList<SystemProcess> Processes { get; init; } = new List<SystemProcess>();
    public IList<IProperty> Properties { get; init; } = new List<IProperty>();
    public IList<Modifier> Modifiers { get; init; } = new List<Modifier>();
}