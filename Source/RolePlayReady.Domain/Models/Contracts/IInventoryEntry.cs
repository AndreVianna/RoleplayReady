namespace RolePlayReady.Models.Contracts;

public interface IInventoryEntry {
    IInventoryItem InventoryItem { get; }
    decimal Quantity { get; }
}