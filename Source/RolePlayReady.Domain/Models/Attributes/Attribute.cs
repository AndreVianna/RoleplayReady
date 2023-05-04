﻿using static System.Results.ValidationResult;

namespace RolePlayReady.Models.Attributes;

public abstract record Attribute<TValue> : IAttribute {
    public required AttributeDefinition Definition { get; init; }

    object? IAttribute.Value => Value;
    public TValue Value { get; init; } = default!;

    public ICollection<ValidationError> Validate() {
        var result = Success();
        result += Definition.DataType.IsNotNull().And.IsEqualTo<TValue>().Errors;
        result += Definition.Constraints.Aggregate(Success(), (r, c)
            => r + c.Create<TValue>(Definition.Name).Validate(Value));
        return result.Errors;
    }
}