﻿namespace System.Validators.Number;

public sealed class MaximumValueExclusive<TValue> : NumberValidator<TValue>
    where TValue : IComparable<TValue> {

    public MaximumValueExclusive(string source, TValue threshold)
        : base(source, threshold) {
    }

    protected override ValidationResult ValidateValue(NumberValidation<TValue> validation, TValue threshold)
        => validation.LessThan(threshold).Result;
}