namespace RoleplayReady.Domain.Models.Contracts;

public interface IPossession {
    IActor Owner { get; init; }
    IObject Object { get; init; }
    decimal Quantity { get; init; }
}