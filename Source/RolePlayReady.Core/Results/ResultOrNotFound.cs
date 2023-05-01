namespace System.Results;

public record ResultOrNotFound : Result {
    private readonly bool _found;

    protected ResultOrNotFound(bool found, IEnumerable<ValidationError>? errors = null)
        : base(errors) {
        _found = found;
    }

    public override bool IsSuccess => IsValid 
        ? base.IsSuccess && !IsNotFound
        : throw new InvalidOperationException("Cannot check for success on an invalid request.");
    public bool IsNotFound => IsValid
        ? !_found
        : throw new InvalidOperationException("Cannot check for not found on an invalid request.");

    public static new ResultOrNotFound AsSuccess() => new(true);
    public static ResultOrNotFound AsNotFound() => new(false);
    public static new ResultOrNotFound<TValue> AsSuccessFor<TValue>(TValue value) => new(value, true);
    public static ResultOrNotFound<TValue> AsNotFoundFor<TValue>(TValue value) => new(value, false);

    public static new ResultOrNotFound AsInvalid(string message, string source)
        => AsInvalid(new ValidationError(message, source));
    public static new ResultOrNotFound AsInvalid(ValidationError error)
        => AsInvalid(new[] { error });
    public static new ResultOrNotFound AsInvalid(IEnumerable<ValidationError> errors)
        => new(true, Ensure.IsNotNullOrEmpty(errors));
    public static new ResultOrNotFound<TValue> AsInvalidFor<TValue>(TValue value, string message, string source)
        => AsInvalidFor(value, new ValidationError(message, source));
    public static new ResultOrNotFound<TValue> AsInvalidFor<TValue>(TValue value, ValidationError error)
        => AsInvalidFor(value, new[] { error });
    public static new ResultOrNotFound<TValue> AsInvalidFor<TValue>(TValue value, IEnumerable<ValidationError> errors)
        => new(value, true, Ensure.IsNotNullOrEmpty(errors));

    public static implicit operator ResultOrNotFound(List<ValidationError> errors)
        => new(false, Ensure.IsNotNullOrEmpty(errors));
    public static implicit operator ResultOrNotFound(ValidationError[] errors)
        => new(false, Ensure.IsNotNullOrEmpty(errors));
    public static implicit operator ResultOrNotFound(ValidationError error)
        => new(false, new[] { error }.AsEnumerable());

    public static ResultOrNotFound operator +(ResultOrNotFound left, Result right)
        => new(!left.IsNotFound, right.Errors.Union(left.Errors));
    public static ResultOrNotFound operator +(Result left, ResultOrNotFound right)
        => new(!right.IsNotFound, left.Errors.Union(right.Errors));
}

public record ResultOrNotFound<TValue> : ResultOrNotFound {
    internal ResultOrNotFound(TValue? value, bool found, IEnumerable<ValidationError>? errors = null)
        : base(found, errors) {
        Value = value;
    }

    public TValue? Value { get; }

    public static implicit operator ResultOrNotFound<TValue>(TValue? value) => new(value, true);
    public static implicit operator TValue?(ResultOrNotFound<TValue> input) => input.Value;

    public static ResultOrNotFound<TValue> operator +(ResultOrNotFound<TValue> left, Result right)
        => new(left.Value, !left.IsNotFound, left.Errors.Union(right.Errors));
    public static ResultOrNotFound<TValue> operator +(Result left, ResultOrNotFound<TValue> right)
        => new(right.Value, !right.IsNotFound, right.Errors.Union(left.Errors));
}
