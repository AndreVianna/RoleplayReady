namespace System.Results;

public record ResultOrNotFound : Result {
    protected ResultOrNotFound(bool found = false, IEnumerable<ValidationError>? errors = null)
        : base(errors) {
        IsNotFound = !found;
    }

    public override bool IsSuccess => !IsNotFound && base.IsSuccess;
    public bool IsNotFound { get; }

    public static new ResultOrNotFound Success() => new(true);
    public static ResultOrNotFound NotFound() => new();
    public static ResultOrNotFound<TValue?> FromValue<TValue>(TValue? value, bool found) => new(value, found);
    public static new ResultOrNotFound<TValue> Success<TValue>(TValue value) => new(value, true);
    public static ResultOrNotFound<TValue?> NotFound<TValue>(TValue? value) => new(value);

    public static ResultOrNotFound WithError(bool found, string message, string source)
        => WithError(found, new ValidationError(message, source));
    public static ResultOrNotFound WithError(bool found, ValidationError error)
        => WithErrors(found, new[] { error });
    public static ResultOrNotFound WithErrors(bool found, IEnumerable<ValidationError> errors)
        => new(found, Ensure.IsNotNullOrEmpty(errors));
    public static ResultOrNotFound<TValue> WithError<TValue>(TValue? value, bool found, string message, string source)
        => WithError(value, found, new ValidationError(message, source));
    public static ResultOrNotFound<TValue> WithError<TValue>(TValue? value, bool found, ValidationError error)
        => WithErrors(value, found, new[] { error });
    public static ResultOrNotFound<TValue> WithErrors<TValue>(TValue? value, bool found, IEnumerable<ValidationError> errors)
        => new(value, found, Ensure.IsNotNullOrEmpty(errors));

    public static implicit operator ResultOrNotFound(List<ValidationError> errors) => new(false, Ensure.IsNotNullOrEmpty(errors));
    public static implicit operator ResultOrNotFound(ValidationError[] errors) => new(false, Ensure.IsNotNullOrEmpty(errors));
    public static implicit operator ResultOrNotFound(ValidationError error) => new(false, new[] { error }.AsEnumerable());

    public static ResultOrNotFound operator +(ResultOrNotFound left, Result right) => new(!left.IsNotFound, right.Errors.Union(left.Errors));
    public static ResultOrNotFound operator +(Result left, ResultOrNotFound right) => new(!right.IsNotFound, left.Errors.Union(right.Errors));
}

public record ResultOrNotFound<TValue> : ResultOrNotFound {
    internal ResultOrNotFound(TValue? value = default, bool found = false, IEnumerable<ValidationError>? errors = null)
        : base(found, errors) {
        Value = value;
    }

    public TValue? Value { get; }

    public static implicit operator ResultOrNotFound<TValue>(TValue? value) => new(value, true);
    public static implicit operator TValue?(ResultOrNotFound<TValue> input) => input.Value;

    public static ResultOrNotFound<TValue> operator +(ResultOrNotFound<TValue> left, Result right) => new(left.Value, !left.IsNotFound, left.Errors.Union(right.Errors));
    public static ResultOrNotFound<TValue> operator +(Result left, ResultOrNotFound<TValue> right) => new(right.Value, !right.IsNotFound, right.Errors.Union(left.Errors));
}
