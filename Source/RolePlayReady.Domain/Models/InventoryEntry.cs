namespace RolePlayReady.Models;

public record InventoryEntry : IInventoryEntry {
    public required IInventoryItem Item { get; init; }
    public required decimal Quantity { get; init; }
}
