namespace RoleplayReady.Domain.Models;

public record RuleSet : Entity, IRuleSet {
    public RuleSet() {
    }

    [SetsRequiredMembers]
    public RuleSet(string ownerId, string abbreviation, string name, string? description = null)
        : base(ownerId, name, description) {
        Abbreviation = abbreviation;
    }

    // Abbreviation also must be unique.
    public required string Abbreviation { get; init; }

    public IList<ISource> Sources { get; init; } = new List<ISource>();
    public IList<IAttribute> Attributes { get; init; } = new List<IAttribute>();
    public IList<IElement> Elements { get; init; } = new List<IElement>();
    public IList<IWorkflow> Workflows { get; init; } = new List<IWorkflow>();
}