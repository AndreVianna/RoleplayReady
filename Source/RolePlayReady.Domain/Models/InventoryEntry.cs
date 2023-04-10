namespace RolePlayReady.Models;

public record InventoryEntry : IInventoryEntry {
    public required IInventoryItem InventoryItem { get; init; }
    public required decimal Quantity { get; init; }
}
