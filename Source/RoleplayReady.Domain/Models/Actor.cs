namespace RoleplayReady.Domain.Models;

public record Actor : Element, IActor {
    public Actor() { }

    [SetsRequiredMembers]
    public Actor(IEntity parent, string ownerId, string abbreviation, string name, string description, State? state = null, Usage? usage = null, ISource? source = null)
        : base(parent, ownerId, abbreviation, name, description, state, usage, source) { }

    [SetsRequiredMembers]
    public Actor(IEntity parent, string ownerId, string name, string description, State? state = null, Usage? usage = null, ISource? source = null) :
        base(parent, ownerId, name, description, state, usage, source) { }

    public IList<IPossession> Possessions { get; init; } = new List<IPossession>();
    public IList<IPower> Powers { get; init; } = new List<IPower>();
    public IList<IAction> Actions { get; init; } = new List<IAction>();
    public IList<ICondition> Conditions { get; init; } = new List<ICondition>();
    public IList<IJournalEntry> JournalEntries { get; init; } = new List<IJournalEntry>();
}