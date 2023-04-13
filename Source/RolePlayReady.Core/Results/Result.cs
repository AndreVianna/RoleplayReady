using System.Results.Abstractions;

namespace System.Results;

public class Result<TValue> : ResultBase<TValue>, IResult<TValue> {
    public Result(object? input)
        : base(input ?? throw new InvalidCastException(string.Format(CannotBeNull, nameof(input)))) {
    }

    public bool HasValue => InternalValue is not null;

    [NotNull]
    public TValue Value => InternalValue ?? throw new InvalidOperationException(string.Format(CannotBeNull, nameof(Value)));

    public static implicit operator Result<TValue>(TValue value) => new(value);
    public static implicit operator Result<TValue>(Validation validation) => new(validation);
    public static implicit operator Result<TValue>(Failure failure) => new(failure.Errors);
    public static implicit operator Result<TValue>(List<ValidationError> errors) => new(errors);
    public static implicit operator Result<TValue>(ValidationError[] errors) => new(errors);
    public static implicit operator Result<TValue>(ValidationError error) => new(error);

    public static implicit operator TValue(Result<TValue> input) => input.Value;

    public static Result<TValue> operator +(Result<TValue> left, Validation right) => (Result<TValue>)left.AddErrors(right.Errors);
}