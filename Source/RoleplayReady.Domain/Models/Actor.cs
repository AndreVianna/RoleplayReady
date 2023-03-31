namespace RoleplayReady.Domain.Models;

public record Actor : Element, IActor {
    public Actor() {

    }

    [SetsRequiredMembers]
    public Actor(IEntity parent, string ownerId, string name, string? description = null)
        : base(parent, ownerId, name, description) { }

    public IList<IBundle> Possessions { get; init; } = new List<IBundle>();
    public IList<IPower> Powers { get; init; } = new List<IPower>();
    public IList<IAction> Actions { get; init; } = new List<IAction>();
    public IList<ICondition> Conditions { get; init; } = new List<ICondition>();
    public IList<IEntry> Journal { get; init; } = new List<IEntry>();
}