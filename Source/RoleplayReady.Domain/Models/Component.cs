namespace RoleplayReady.Domain.Models;

public abstract record Component : Entity, IComponent {
    protected Component() { }

    [SetsRequiredMembers]
    protected Component(IComponent? parent, string abbreviation, string name, string description, IDateTimeProvider? dateTime = null)
        : base(abbreviation, name, description, dateTime) {
        Parent = parent;
    }

    public IComponent? Parent { get; init; } // Top level should be a RuleSet.
    public IComponent? Root { get; init; } // Quick access to the root.
    public IList<IComponent> Components { get; init; } = new List<IComponent>();

    public override TSelf CloneUnder<TSelf>(IEntity? parent) {
        var result = base.CloneUnder<Component>(Parent);
        foreach (var child in Components.Cast<Component>())
            result.Components.Add(child.CloneUnder<IComponent>(result));
        return (result as TSelf)!;
    }
}