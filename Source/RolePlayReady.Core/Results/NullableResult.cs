using System.Results.Abstractions;

namespace System.Results;

public class NullableResult<TValue> : ResultBase<TValue>, IMaybe<TValue> {
    public NullableResult() : base(default) { }

    public NullableResult(object? input) : base(input) {
    }

    public bool HasValue => InternalValue is not null;
    public bool IsNull => InternalValue is null;

    public TValue? Value => InternalValue;

    public static implicit operator NullableResult<TValue>(TValue? value) => new(value);
    public static implicit operator NullableResult<TValue>(ValidationResult validationResult) => new(validationResult);
    public static implicit operator NullableResult<TValue>(Failure failure) => new(failure.Errors);
    public static implicit operator NullableResult<TValue>(List<ValidationError> errors) => new(errors);
    public static implicit operator NullableResult<TValue>(ValidationError[] errors) => new(errors);
    public static implicit operator NullableResult<TValue>(ValidationError error) => new(error);

    public static implicit operator TValue?(NullableResult<TValue> input) => input.Value;

    public static NullableResult<TValue> operator +(NullableResult<TValue> left, ValidationResult right) => (NullableResult<TValue>)left.AddErrors(right.Errors);
}