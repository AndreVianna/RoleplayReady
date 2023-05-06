using static System.Results.ValidationResult;

namespace RolePlayReady.Models.Attributes;

public abstract record Attribute<TValue> : IAttribute {
    public required AttributeDefinition Definition { get; init; }

    object? IAttribute.Value => Value;
    public TValue Value { get; init; } = default!;

    public ValidationResult ValidateSelf(bool negate = false) {
        var result = Success();
        result += Definition.DataType.IsRequired().And().IsEqualTo<TValue>().Result;
        result += Definition.Constraints.Aggregate(Success(), (r, c)
            => r + c.Create(Value, Definition.Name).Validate());
        return result;
    }
}