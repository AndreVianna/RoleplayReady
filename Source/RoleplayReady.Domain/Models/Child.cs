namespace RoleplayReady.Domain.Models;

public abstract record Child : Entity, IChild {
    protected Child() {

    }

    [SetsRequiredMembers]
    protected Child(IEntity parent, string ownerId, string name, string? description = null, Status? status = null)
        : base(ownerId, name, description, status) {
        Parent = parent ?? throw new ArgumentNullException(nameof(parent));
        RuleSet = parent is IChild element ? element.RuleSet : parent as RuleSet ?? throw new ArgumentException("Parent must be a RuleSet or one of its Children.", nameof(parent));
    }

    public required RuleSet RuleSet { get; init; }
    public required IEntity? Parent { get; init; }
}