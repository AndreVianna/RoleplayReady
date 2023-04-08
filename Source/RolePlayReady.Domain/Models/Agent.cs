namespace RolePlayReady.Models;

public record Agent : Entity, IAgent {
    public IList<IPossession> Possessions { get; init; } = new List<IPossession>();
    public IList<IJournalEntry> JournalEntries { get; init; } = new List<IJournalEntry>();
}