using System.Results.Abstractions;

namespace System.Results;

public class Maybe<TValue> : ResultBase<TValue>, IMaybe<TValue> {
    public Maybe() : base(default) {}

    public Maybe(object? input) : base(input) {
    }

    public bool HasValue => InternalValue is not null;
    public bool IsNull => InternalValue is null;

    public TValue? Value => InternalValue;

    public static implicit operator Maybe<TValue>(TValue? value) => new(value);
    public static implicit operator Maybe<TValue>(Validation validation) => new(validation);
    public static implicit operator Maybe<TValue>(Failure failure) => new(failure.Errors);
    public static implicit operator Maybe<TValue>(List<ValidationError> errors) => new(errors);
    public static implicit operator Maybe<TValue>(ValidationError[] errors) => new(errors);
    public static implicit operator Maybe<TValue>(ValidationError error) => new(error);

    public static implicit operator TValue?(Maybe<TValue> input) => input.Value;

    public static Maybe<TValue> operator +(Maybe<TValue> left, Validation right) => (Maybe<TValue>)left.AddErrors(right.Errors);
}