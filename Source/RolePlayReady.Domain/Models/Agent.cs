namespace RolePlayReady.Models;

public record Agent : Component {
    public IList<InventoryEntry> Inventory { get; init; } = [];
    public IList<JournalEntry> Journal { get; init; } = [];
}