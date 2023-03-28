namespace RoleplayReady.Domain.Models;

public record Feature<TValue> : IFeature
{
    // System and Name must be unique.
    public required GameSystem System { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public Type Type => typeof(TValue);
}