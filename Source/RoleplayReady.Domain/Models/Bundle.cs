namespace RoleplayReady.Domain.Models;

public record Bundle {
    public required Object Object { get; init; }
    public required decimal Quantity { get; init; }
    public required string Unit { get; init; }
}
