namespace RolePlayReady.Models.Abstractions;

public interface IInventoryItem : IEntity<Guid> {
    string Unit { get; }
}