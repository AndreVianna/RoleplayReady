namespace RoleplayReady.Domain.Models.Contracts;

public interface IActor : IHavePowers {
    IList<IPossession> Possessions { get; init; }
    IList<IAction> Actions { get; init; }
    IList<ICondition> Conditions { get; init; }
    IList<IJournalEntry> JournalEntries { get; init; }
}