namespace RolePlayReady.Models.Contracts;

public interface IActor : IComponent {
    IList<IPossession> Possessions { get; init; }
    IList<IJournalEntry> JournalEntries { get; init; }
}