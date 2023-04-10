namespace RolePlayReady.Models.Abstractions;

public interface IInventoryItem : IEntity {
    string Unit { get; }
}