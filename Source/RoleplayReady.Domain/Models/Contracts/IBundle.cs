namespace RoleplayReady.Domain.Models.Contracts;

public interface IBundle {
    IObject Object { get; init; }
    decimal Quantity { get; init; }
    string Unit { get; init; }
}