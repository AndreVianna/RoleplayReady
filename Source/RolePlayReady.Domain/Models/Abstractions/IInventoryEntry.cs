namespace RolePlayReady.Models.Abstractions;

public interface IInventoryEntry {
    IInventoryItem Item { get; }
    decimal Quantity { get; }
}