namespace RoleplayReady.Domain.Models;

public record Modifier
{
    public required string Name { get; init; }
    public required GameSystem GameSystem { get; init; }
    public required string Description { get; init; }
    public required string ModifierType { get; init; }
    public required string Value { get; init; }
    public required string SourceId { get; init; }
}
