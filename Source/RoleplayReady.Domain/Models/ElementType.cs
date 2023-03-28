namespace RoleplayReady.Domain.Models;

public record ElementType
{
    public required string Name { get; init; }
    // Name must be unique across all systems.

    public required string Description { get; init; }
}