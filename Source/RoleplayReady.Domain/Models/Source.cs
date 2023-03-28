namespace RoleplayReady.Domain.Models;

public class Source
{
    // System and Name must be unique.
    public required GameSystem System { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string OwnerId { get; init; }
}
