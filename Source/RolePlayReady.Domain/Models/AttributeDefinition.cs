namespace RolePlayReady.Models;

public record AttributeDefinition : IAttributeDefinition {
    public required string Name { get; init; }
    public string? ShortName { get; init; }
    public required string Description { get; init; }
    public string FullName => $"{Name}{(ShortName is null ? "" : $" ({ShortName})")} : {DataType.Name}";
    public required Type DataType { get; init; }
}