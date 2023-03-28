namespace RoleplayReady.Domain.Models;

public record GameSystem
{
    // Abbreviation must be unique.
    public required string Abbreviation { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string Publisher { get; init; }
    public required string OwnerId { get; init; }

    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
    public SystemStatus Status { get; init; }

    public IList<Source> Sources { get; init; } = new List<Source>();
    public IList<ElementType> ElementTypes { get; init; } = new List<ElementType>();
    public IList<SystemProcess> Processes { get; init; } = new List<SystemProcess>();
    public IList<IFeature> Features { get; init; } = new List<IFeature>();
    public IList<Modifier> Modifiers { get; init; } = new List<Modifier>();
}