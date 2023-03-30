namespace RoleplayReady.Domain.Models;

public record RuleSet
{
    // Abbreviation must be unique.
    public required string OwnerId { get; init; }
    public required string Name { get; init; }
    public required string Abbreviation { get; init; }
    public string? Description { get; init; }
    public string? Publisher { get; init; }

    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
    public Status Status { get; init; }

    public IList<Source> Sources { get; init; } = new List<Source>();
    public IList<ElementType> ElementTypes { get; init; } = new List<ElementType>();
    public IList<Element> Elements { get; init; } = new List<Element>();
    public IList<RuleSetProcess> Processes { get; init; } = new List<RuleSetProcess>();
    public IList<IAttribute> Attributes { get; init; } = new List<IAttribute>();
}