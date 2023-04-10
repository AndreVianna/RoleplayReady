namespace RolePlayReady.Models;

public record InventoryItem : Entity, IInventoryItem {
    public required string Unit { get; init; }
}