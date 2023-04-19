using static System.Validators.ValidatorFactory;

namespace RolePlayReady.Models.Attributes;

public abstract record EntityAttribute<TValue> : IEntityAttribute, IValidatable {
    public required IAttributeDefinition Attribute { get; init; }

    object? IEntityAttribute.Value => Value;
    public TValue Value { get; init; } = default!;

    public ValidationResult Validate() {
        var result = new ValidationResult();
        result += Attribute.DataType.IsNotNull().And.IsEqualTo(typeof(TValue)).Result;
        result += Attribute.Constraints.Aggregate(new ValidationResult(), (r, c)
            => r + For(Attribute.Name)
                .Create(typeof(TValue), c.ValidatorName, c.Arguments.ToArray())
                .Validate(Value));
        return result;
    }
}