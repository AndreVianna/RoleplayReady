namespace RolePlayReady.Models;

public record GameItem : Entity<Guid>, IInventoryItem {
    public GameItem(IDateTime? dateTime = null)
        : base(dateTime) {
    }

    public required string Unit { get; init; }
}