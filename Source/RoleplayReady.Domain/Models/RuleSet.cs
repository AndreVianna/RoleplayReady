namespace RoleplayReady.Domain.Models;

public record RuleSet : Entity, IRuleSet {
    public RuleSet() {
    }

    [SetsRequiredMembers]
    public RuleSet(string ownerId, string abbreviation, string name, string? description = null, Status? status = null)
        : base(ownerId, name, description, status) {
        Abbreviation = abbreviation;
    }

    // Abbreviation also must be unique.
    public required string Abbreviation { get; init; }

    public IList<ISource> Sources { get; } = new List<ISource>();
    public IList<IAttribute> Attributes { get; } = new List<IAttribute>();
    public IList<IElement> Elements { get; } = new List<IElement>();
    public IList<IWorkflow> Workflows { get; } = new List<IWorkflow>();
}