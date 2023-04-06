namespace RolePlayReady.Models.Contracts;

public interface IAgent : IComponent {
    IList<IPossession> Possessions { get; init; }
    IList<IJournalEntry> JournalEntries { get; init; }
}