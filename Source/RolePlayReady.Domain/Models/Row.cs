namespace RolePlayReady.Models;

public record Row : IKey {
    public required Guid Id { get; init; }
    public string? Name { get; init; }
}