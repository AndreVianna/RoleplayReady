namespace RolePlayReady.Models;

public record Agent : Entity, IAgent {
    public Agent(IDateTime? dateTime = null)
        : base(dateTime) {
    }

    public IList<IInventoryEntry> Inventory { get; init; } = new List<IInventoryEntry>();
    public IList<IJournalEntry> Journal { get; init; } = new List<IJournalEntry>();
}