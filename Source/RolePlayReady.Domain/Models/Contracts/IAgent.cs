namespace RolePlayReady.Models.Contracts;

public interface IAgent : INode {
    IList<IPossession> Possessions { get; init; }
    IList<IJournalEntry> JournalEntries { get; init; }
}