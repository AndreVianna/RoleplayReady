namespace RolePlayReady.Models.Abstractions;

public interface IAgent : IEntity {
    IList<IInventoryEntry> Inventory { get; init; }
    IList<IJournalEntry> Journal { get; init; }
}