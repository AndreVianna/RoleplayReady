namespace RolePlayReady.Models;

public record AttributeDefinition : IAttributeDefinition {
    public required string Name { get; init; }
    public string? ShortName { get; init; }
    public required string Description { get; init; }
    public required Type DataType { get; init; }

    public sealed override string ToString() => $"[{GetType().Name}] {Name}{(ShortName is not null ? $" ({ShortName})" : string.Empty)}: {DataType.Name}";
}