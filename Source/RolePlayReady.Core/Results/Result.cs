namespace System.Results;

public record Result : IResult {
    protected Result(IEnumerable<ValidationError>? errors = null) {
        Errors = errors?.ToList() ?? new List<ValidationError>();
    }

    public bool IsValid => Errors.Count == 0;
    public virtual bool IsSuccess => IsValid;
    public bool HasErrors => Errors.Count != 0;
    public IList<ValidationError> Errors { get; protected init; } = new List<ValidationError>();

    public static Result AsSuccess()  => new();
    public static Result<TValue> AsSuccessFor<TValue>(TValue value) => new(value);

    public static Result AsInvalid(string message, string source)
        => AsInvalid(new ValidationError(message, source));
    public static Result AsInvalid(ValidationError error)
        => AsInvalid(new [] { error });
    public static Result AsInvalid(IEnumerable<ValidationError> errors)
        => new(Ensure.IsNotNull(errors));
    public static Result<TValue> AsInvalidFor<TValue>(TValue value, string message, string source)
        => AsInvalidFor(value, new ValidationError(message, source));
    public static Result<TValue> AsInvalidFor<TValue>(TValue value, ValidationError error)
        => AsInvalidFor(value, new[] { error });
    public static Result<TValue> AsInvalidFor<TValue>(TValue value, IEnumerable<ValidationError> errors)
        => new(value, Ensure.IsNotNull(errors));

    public static implicit operator Result(List<ValidationError> errors) => new(errors.AsEnumerable());
    public static implicit operator Result(ValidationError[] errors) => new(errors.AsEnumerable());
    public static implicit operator Result(ValidationError error) => new(new[] { error }.AsEnumerable());
    public static implicit operator bool(Result result) => result.IsSuccess;

    public static Result operator +(Result left, Result right) => new(right.Errors.Union(left.Errors));

    public virtual bool Equals(Result? other)
        => other is not null
        && Errors.SequenceEqual(other.Errors);

    public override int GetHashCode()
        => Errors.Aggregate(Array.Empty<ValidationError>().GetHashCode(), HashCode.Combine);
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

    public virtual bool Equals(Result<TValue>? other)
        => other is not null
        && (Value?.Equals(other.Value) ?? other.Value is null)
        && Errors.SequenceEqual(other.Errors);

    public override int GetHashCode()
        => HashCode.Combine(base.GetHashCode(), Value);
}
