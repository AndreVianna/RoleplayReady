namespace System.Results;

public record Result : ResultBase {
    protected Result(IEnumerable<ValidationError>? errors = null) {
        Errors = errors?.ToList() ?? new List<ValidationError>();
    }

    public static Result Success()  => new();
    public static Result<TValue> Success<TValue>(TValue value) => new(value);

    public static Result FromError(string message, string source)
        => FromError(new ValidationError(message, source));
    public static Result FromError(ValidationError error)
        => FromErrors(new [] { error });
    public static Result FromErrors(IEnumerable<ValidationError> errors)
        => new(Ensure.IsNotNull(errors));
    public static Result<TValue?> WithError<TValue>(TValue? value, string message, string source)
        => WithError(value, new ValidationError(message, source));
    public static Result<TValue?> WithError<TValue>(TValue? value, ValidationError error)
        => WithErrors(value, new[] { error });
    public static Result<TValue?> WithErrors<TValue>(TValue? value, IEnumerable<ValidationError> errors)
        => new(value, Ensure.IsNotNull(errors));

    public static implicit operator Result(List<ValidationError> errors) => new(errors.AsEnumerable());
    public static implicit operator Result(ValidationError[] errors) => new(errors.AsEnumerable());
    public static implicit operator Result(ValidationError error) => new(new[] { error }.AsEnumerable());
    public static implicit operator bool(Result result) => result.IsSuccess;

    public static Result operator +(Result left, Result right) => new(right.Errors.Union(left.Errors));
}

public record Result<TValue> : Result, IResult<TValue> {
    internal Result(TValue? value, IEnumerable<ValidationError>? errors = null)
        : base(errors) {
        Value = value;
    }

    public TValue? Value { get; }

    public static implicit operator Result<TValue>(TValue? value) => new(value, Array.Empty<ValidationError>());
    public static implicit operator TValue?(Result<TValue> input) => input.Value;

    public static Result<TValue> operator +(Result<TValue> left, Result right) => new(left.Value, left.Errors.Union(right.Errors));
    public static Result<TValue> operator +(Result left, Result<TValue> right) => new(right.Value, right.Errors.Union(left.Errors));
}
