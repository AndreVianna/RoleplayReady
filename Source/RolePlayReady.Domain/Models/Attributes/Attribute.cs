﻿using static System.Validators.ValidatorFactory;

namespace RolePlayReady.Models.Attributes;

public abstract record Attribute<TValue> : IAttribute {
    public required AttributeDefinition Definition { get; init; }

    object? IAttribute.Value => Value;
    public TValue Value { get; init; } = default!;

    public ValidationResult Validate() {
        var result = ValidationResult.AsSuccess();
        result += Definition.DataType.IsNotNull().And.IsEqualTo<TValue>().Result;
        result += Definition.Constraints.Aggregate(ValidationResult.AsSuccess(), (r, c)
            => r + For(Definition.Name)
                .Create(typeof(TValue), c.ValidatorName, c.Arguments.ToArray())
                .Validate(Value));
        return result;
    }
}