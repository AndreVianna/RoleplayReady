namespace RolePlayReady.Models;

public record GameItem : Entity, IInventoryItem {
    public GameItem(IDateTime? dateTime = null)
        : base(dateTime) {
    }

    public required string Unit { get; init; }
}