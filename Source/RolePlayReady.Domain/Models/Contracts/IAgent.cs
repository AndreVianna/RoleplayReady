namespace RolePlayReady.Models.Contracts;

public interface IAgent : IEntity {
    IList<IPossession> Possessions { get; init; }
    IList<IJournalEntry> JournalEntries { get; init; }
}