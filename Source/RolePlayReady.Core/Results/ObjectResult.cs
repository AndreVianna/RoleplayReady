using System.Results.Abstractions;

namespace System.Results;

public class ObjectResult<TValue> : ResultBase<TValue>, IResult<TValue> {
    public ObjectResult(object? input)
        : base(input ?? throw new InvalidCastException(string.Format(CannotBeNull, nameof(input)))) {
    }

    public bool HasValue => InternalValue is not null;

    [NotNull]
    public TValue Value => InternalValue ?? throw new InvalidOperationException(string.Format(CannotBeNull, nameof(Value)));

    public static implicit operator ObjectResult<TValue>(TValue value) => new(value);
    public static implicit operator ObjectResult<TValue>(ValidationResult validationResult) => new(validationResult);
    public static implicit operator ObjectResult<TValue>(Failure failure) => new(failure.Errors);
    public static implicit operator ObjectResult<TValue>(List<ValidationError> errors) => new(errors);
    public static implicit operator ObjectResult<TValue>(ValidationError[] errors) => new(errors);
    public static implicit operator ObjectResult<TValue>(ValidationError error) => new(error);

    public static implicit operator TValue(ObjectResult<TValue> input) => input.Value;

    public static ObjectResult<TValue> operator +(ObjectResult<TValue> left, ValidationResult right) => (ObjectResult<TValue>)left.AddErrors(right.Errors);
}