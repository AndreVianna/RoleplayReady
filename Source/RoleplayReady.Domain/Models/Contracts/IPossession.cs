namespace RolePlayReady.Models.Contracts;

public interface IPossession {
    IObject Object { get; init; }
    decimal Quantity { get; init; }
}