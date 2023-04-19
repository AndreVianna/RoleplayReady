using RolePlayReady.Models.Attributes;

namespace RolePlayReady.Models.Abstractions;

public interface IEntityAttribute<TValue> : IEntityAttribute {
    new AttributeDefinition<TValue> Attribute { get; }
    new TValue? Value { get; }
}

public interface IEntityAttribute {
    IAttributeDefinition Attribute { get; }
    object? Value { get; }
    bool IsValid { get; }
}
