namespace RoleplayReady.Domain.Models;

public record Process
{
    // Name and ElementType must be unique.
    public required string Name { get; init; }
    public required ElementType ElementType { get; init; }
    public required string Description { get; init; }
}