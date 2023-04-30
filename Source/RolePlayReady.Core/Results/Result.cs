namespace System.Results;

public record Result : ResultBase {
    public Result(IEnumerable<ValidationError>? errors = null) {
        Errors = errors?.ToList() ?? new List<ValidationError>();
    }

    public static Result Success { get; } = new();
    public static Result<TValue> FromValue<TValue>(TValue? value) => new(value);

    public static Result Failure(string message, string source)  => Failure(new ValidationError(message, source));
    public static Result Failure(ValidationError error) => Failure(new [] { error });
    public static Result Failure(IEnumerable<ValidationError> errors) => new(errors);
    public static Result<TValue> Failure<TValue>(TValue value, string message, string source) => Failure(value, new ValidationError(message, source));
    public static Result<TValue> Failure<TValue>(TValue value, ValidationError error) => Failure(value, new[] { error });
    public static Result<TValue> Failure<TValue>(TValue value, IEnumerable<ValidationError> errors) => new(value, errors);

    public static implicit operator Result(List<ValidationError> errors) => new(errors.AsEnumerable());
    public static implicit operator Result(ValidationError[] errors) => new(errors.AsEnumerable());
    public static implicit operator Result(ValidationError error) => new(new[] { error }.AsEnumerable());
    public static implicit operator bool(Result result) => result.IsSuccess;

    public static Result operator +(Result left, Result right) => new(right.Errors.Union(left.Errors));

    public virtual bool Equals(Result? other)
        => other is not null
        && Errors.SequenceEqual(other.Errors);

    public override int GetHashCode()
        => Errors.Aggregate(Errors.GetHashCode(), HashCode.Combine);
}

public record Result<TValue> : Result, IResult<TValue> {
    public Result(TValue? input)
        : this((object?)input) {
    }

    public Result(object? input, IEnumerable<ValidationError>? errors = null)
        : base(errors) {
        Value = input switch {
            TValue value => value,
            null => default!,
            _ => throw new InvalidCastException(string.Format(CannotAssign, $"Result<{typeof(TValue).GetName()}>", $"Result<{input.GetType().GetName()}>"))
        };
    }

    public TValue Value { get; }

    public static implicit operator Result<TValue>(Result<object?> value) => new(value.Value, value.Errors);
    public static implicit operator Result<TValue>(TValue? value) => new(value, Array.Empty<ValidationError>());
    public static implicit operator TValue(Result<TValue> input) => input.Value;

    public static Result<TValue> operator +(Result<TValue> left, Result right) => new(left.Value, left.Errors.Union(right.Errors));
    public static Result<TValue> operator +(Result left, Result<TValue> right) => new(right.Value, right.Errors.Union(left.Errors));

    public static bool operator ==(Result<TValue> left, Result right) => ReferenceEquals(right, Result.Success) && left.IsSuccess;
    public static bool operator !=(Result<TValue> left, Result right) => !ReferenceEquals(right, Result.Success) || !left.IsSuccess;

    public virtual bool Equals(Result<TValue>? other)
        => other is not null
           && (Value?.Equals(other.Value) ?? other.Value is null)
           && Errors.SequenceEqual(other.Errors);

    public override int GetHashCode()
        => HashCode.Combine(base.GetHashCode(), Value);
}
