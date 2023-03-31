namespace RoleplayReady.Domain.Models;

public abstract record Child : Entity, IChild {
    protected Child() {

    }

    [SetsRequiredMembers]
    protected Child(IEntity parent, string ownerId, string name, string? description = null)
        : base(ownerId, name, description) {
        Parent = parent ?? throw new ArgumentNullException(nameof(parent));
        RuleSet = parent is Element element ? element.RuleSet : parent as RuleSet ?? throw new ArgumentException("Parent must be an Element or a RuleSet.", nameof(parent));
    }

    public required RuleSet RuleSet { get; init; }
    public required IEntity? Parent { get; init; }
}