namespace RolePlayReady.Models.Contracts;

public interface IInventoryItem : IEntity {
    string Unit { get; }
}