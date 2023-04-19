namespace RolePlayReady.Models.Abstractions;

public interface IEntityAttribute {
    IAttributeDefinition Attribute { get; }
    object? Value { get; }
}
