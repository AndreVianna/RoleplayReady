namespace RoleplayReady.Domain.Models;

public abstract record EntityProperty
{
    public required string Name { get; init; }
    public required GameSystem GameSystem { get; init; }
}