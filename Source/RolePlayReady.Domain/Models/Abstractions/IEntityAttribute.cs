namespace RolePlayReady.Models.Abstractions;

public interface IEntityAttribute {
    IAttributeDefinition AttributeDefinition { get; }
    object? Value { get; }
    bool IsValid { get; }
}
