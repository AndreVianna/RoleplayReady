using static System.Results.ValidationResult;

namespace RolePlayReady.Models.Attributes;

public abstract record EntityAttribute<TValue> : IEntityAttribute {
    public required IAttributeDefinition AttributeDefinition { get; init; }
    object? IEntityAttribute.Value => Value;
    public TValue Value { get; set; } = default!;
    public bool IsValid => AttributeDefinition.Constraints.All(c => c.Validate(this) == Success);
}