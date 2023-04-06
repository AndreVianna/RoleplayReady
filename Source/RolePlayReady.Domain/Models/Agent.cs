using RolePlayReady.Utilities.Contracts;

namespace RolePlayReady.Models;

public record Agent : Component, IAgent {
    public Agent() { }

    [SetsRequiredMembers]
    public Agent(IComponent? parent, string abbreviation, string name, string description, IDateTimeProvider? dateTime = null)
        : base(parent, abbreviation, name, description, dateTime) { }

    public override string Type => nameof(Agent);

    public IList<IPossession> Possessions { get; init; } = new List<IPossession>();
    public IList<IPower> Powers { get; init; } = new List<IPower>();
    public IList<IAction> Actions { get; init; } = new List<IAction>();
    public IList<ICondition> Conditions { get; init; } = new List<ICondition>();
    public IList<IJournalEntry> JournalEntries { get; init; } = new List<IJournalEntry>();
}