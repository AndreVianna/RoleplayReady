namespace RolePlayReady.Models;

public record Agent : Entity<Guid>, IAgent {
    public Agent(IDateTime? dateTime = null)
        : base(dateTime) {
    }

    public IList<InventoryEntry> Inventory { get; init; } = new List<InventoryEntry>();
    public IList<JournalEntry> Journal { get; init; } = new List<JournalEntry>();
}