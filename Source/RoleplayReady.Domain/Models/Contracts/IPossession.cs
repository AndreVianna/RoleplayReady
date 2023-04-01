namespace RoleplayReady.Domain.Models.Contracts;

public interface IPossession : IAmChildOf {
    IObject Object { get; init; }
    decimal Quantity { get; init; }
}