namespace RoleplayReady.Domain.Models;

public record RuleSet : Entity, IRuleSet {
    public RuleSet() {
    }

    [SetsRequiredMembers]
    public RuleSet(string ownerId, string abbreviation, string name, string description, State? state = null)
        : base(ownerId, abbreviation, name, description, state) {
    }

    public IList<ISource> Sources { get; } = new List<ISource>();
    public IList<IComponent> Components { get; } = new List<IComponent>();
    public IList<IActor> Actors { get; } = new List<IActor>();
    public IList<IProcess> Workflows { get; } = new List<IProcess>();
}