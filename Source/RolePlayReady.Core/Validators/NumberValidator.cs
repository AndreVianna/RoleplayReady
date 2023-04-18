﻿namespace System.Validators;

public abstract class NumberValidator<TNumber> : IValidator
    where TNumber : IComparable<TNumber> {
    private readonly string _source;
    private readonly TNumber _threshold;

    protected NumberValidator(string source, TNumber threshold) {
        _source = source;
        _threshold = threshold;
    }

    public ValidationResult Validate<TValue>(TValue? input) {
        if (input is TNumber value) {
            var validation = new NumberValidation<TNumber>(value, _source);
            return ValidateNumber(validation, _threshold);
        }

        return input is null
            ? new ValidationError(CannotBeNull, _source)
            : new ValidationError(IsNotOfType, _source, typeof(TValue).Name, input.GetType().Name);
    }

    protected abstract ValidationResult ValidateNumber(NumberValidation<TNumber> validation, TNumber threshold);
}