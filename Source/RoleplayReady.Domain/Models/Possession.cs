namespace RolePlayReady.Models;

public record Possession : IPossession {
    public required IObject Object { get; init; }
    public required decimal Quantity { get; init; }
}
