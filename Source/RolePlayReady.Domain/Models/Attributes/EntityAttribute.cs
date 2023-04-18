using static System.Results.ValidationResult;
using static System.Validators.ValidatorFactory;

namespace RolePlayReady.Models.Attributes;

public abstract record EntityAttribute<TValue> : IEntityAttribute {
    public required IAttributeDefinition Attribute { get; init; }
    object? IEntityAttribute.Value => Value;
    public string ValueType { get; set; } = default!;
    public TValue Value { get; set; } = default!;
    public bool IsValid => Attribute.Constraints.All(c
        => For(Attribute.Name)
          .Create<TValue>(ValueType, c.ValidatorName, c.Arguments)
          .Validate(Value) == Success);
}