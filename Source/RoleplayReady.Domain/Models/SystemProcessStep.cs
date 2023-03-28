namespace RoleplayReady.Domain.Models;

public record SystemProcessStep
{
    // SystemProcess and Order must be unique.
    public required SystemProcess SystemProcess { get; init; }
    public required int Order { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
}