namespace RolePlayReady.Models.Abstractions;

public interface IAgent : IEntity<Guid> {
    IList<InventoryEntry> Inventory { get; init; }
    IList<JournalEntry> Journal { get; init; }
}