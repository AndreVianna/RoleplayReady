namespace RolePlayReady.Models.Contracts;

public interface IPossession {
    IAgent Owner { get; init; }
    IObject Object { get; init; }
    decimal Quantity { get; init; }
}