namespace RoleplayReady.Domain.Models;

public record Set
{
    public required Item Item { get; init; }
    public required decimal Quantity { get; init; }
    public required string Unit { get; init; }
}
