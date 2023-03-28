namespace RoleplayReady.Domain.Models;

public record Property<TValue> : IProperty
{
    // System and Name must be unique.
    public required System System { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public Type Type => typeof(TValue);
}