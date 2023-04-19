using static System.Results.ValidationResult;
using static System.Validators.ValidatorFactory;

namespace RolePlayReady.Models.Attributes;

public abstract record EntityAttribute<TValue> : IEntityAttribute<TValue> {
    IAttributeDefinition IEntityAttribute.Attribute => Attribute;
    public required AttributeDefinition<TValue> Attribute { get; init; }
    object? IEntityAttribute.Value => Value;
    public TValue Value { get; set; } = default!;
    public bool IsValid => Attribute.Constraints.All(c
        => For(Attribute.Name)
          .Create(typeof(TValue), c.ValidatorName, c.Arguments.ToArray())
          .Validate(Value) == Success);
}