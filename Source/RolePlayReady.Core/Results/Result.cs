using System.Extensions;

namespace System.Results;

public sealed record Result : ResultBase {
    public Result() : this((object?)null) { }

    private Result(object? input) {
        var errors = input switch {
            IEnumerable<ValidationError> inputErrors => Ensure.IsNotNullAndHasNoNullItems(inputErrors, nameof(input)),
            ValidationError error => new[] { Ensure.IsNotNull(error, nameof(input)) },
            _ => Array.Empty<ValidationError>(),
        };

        foreach (var error in errors)
            Errors.Add(error);
    }

    public static Result Success { get; } = new();
    public static Result<TValue> Value<TValue>(TValue value) => new(value);
    public static NullableResult<TValue> NullableValue<TValue>(TValue? value) => new(value);
    public static Result Failure(string message, string source)  => Failure(new ValidationError(message, source));
    public static Result Failure(ValidationError error) => Failure(new [] { error });
    public static Result Failure(IEnumerable<ValidationError> errors) => new(errors);
    public static Result<TValue> Failure<TValue>(TValue value, string message, string source) => Failure(value, new ValidationError(message, source));
    public static Result<TValue> Failure<TValue>(TValue value, ValidationError error) => Failure(value, new[] { error });
    public static Result<TValue> Failure<TValue>(TValue value, IEnumerable<ValidationError> errors) => new Result<TValue>(value) + new Result(errors);
    public static NullableResult<TValue> Failure<TValue>(string message, string source) => Failure<TValue>(new ValidationError(message, source));
    public static NullableResult<TValue> Failure<TValue>(ValidationError error) => Failure<TValue>(new[] { error });
    public static NullableResult<TValue> Failure<TValue>(IEnumerable<ValidationError> errors) => new NullableResult<TValue>() + new Result(errors);

    public static implicit operator Result(List<ValidationError> errors) => new((object?)errors);
    public static implicit operator Result(ValidationError[] errors) => new((object?)errors);
    public static implicit operator Result(ValidationError error) => new((object?)error);
    public static implicit operator bool(Result result) => result.IsSuccess;

    public static Result operator +(Result left, Result right) => right with { Errors = right.Errors.Union(left.Errors).ToList() };

#pragma warning disable CS8851 // Not required
    public bool Equals(Result? other)
        => other is not null
           && (ReferenceEquals(other, Success)
               ? IsSuccess
               : Errors.SequenceEqual(other.Errors));
#pragma warning restore CS8851
}

public record Result<TValue> : ResultBase, IResult<TValue> {
    public Result(TValue input) : this(Ensure.IsNotNull(input), Array.Empty<ValidationError>()) {
    }

    private Result(object? input, IEnumerable<ValidationError> errors) {
        Value = input switch {
            TValue value => value,
            null => throw new InvalidCastException(string.Format(CannotAssignNull, $"Result<{typeof(TValue).GetName()}>")),
            _ => throw new InvalidCastException(string.Format(CannotAssign, $"Result<{typeof(TValue).GetName()}>", $"Result<{input.GetType().GetName()}>"))
        };

        foreach (var error in errors)
            Errors.Add(error);
    }

    [NotNull]
    public TValue Value { get; }

    public static implicit operator Result<TValue>(NullableResult<object> value) => new(value.Value, value.Errors);
    public static implicit operator Result<TValue>(Result<object> value) => new(value.Value, value.Errors);
    public static implicit operator Result<TValue>(TValue? value) => new(value, Array.Empty<ValidationError>());
    public static implicit operator TValue(Result<TValue> input) => input.Value;

    public static Result<TValue> operator +(Result<TValue> left, Result right) => left with { Errors = left.Errors.Union(right.Errors).ToList() };
    public static Result<TValue> operator +(Result left, Result<TValue> right) => right with { Errors = right.Errors.Union(left.Errors).ToList() };

    public static bool operator ==(Result<TValue> left, Result right) => ReferenceEquals(right, Result.Success) && left.IsSuccess;
    public static bool operator !=(Result<TValue> left, Result right) => !ReferenceEquals(right, Result.Success) || !left.IsSuccess;

    public virtual bool Equals(Result<TValue>? other)
        => other is not null
           && Value.Equals(other.Value)
           && Errors.SequenceEqual(other.Errors);

    public override int GetHashCode()
        => HashCode.Combine(base.GetHashCode(), Value);
}

public record NullableResult<TValue> : ResultBase, INullableResult<TValue> {
    public NullableResult(TValue? input = default)
        : this(input, Array.Empty<ValidationError>()) {
    }

    private NullableResult(object? input, IEnumerable<ValidationError> errors) {
        Value = input is null or TValue
            ? (TValue?)input
            : throw new InvalidCastException(string.Format(CannotAssign, $"Result<{typeof(TValue).GetName()}>", $"Result<{input.GetType().GetName()}>"));
        foreach (var error in errors)
            Errors.Add(error);
    }

    public TValue? Value { get; }

    public static implicit operator NullableResult<TValue>(Result<TValue> value) => new(value.Value, value.Errors);
    public static implicit operator NullableResult<TValue>(NullableResult<object> value) => new(value.Value, value.Errors);
    public static implicit operator NullableResult<TValue>(TValue? value) => new(value, Array.Empty<ValidationError>());
    public static implicit operator TValue?(NullableResult<TValue> input) => input.Value;

    public static NullableResult<TValue> operator +(NullableResult<TValue> left, Result right) => left with { Errors = left.Errors.Union(right.Errors).ToList() };
    public static NullableResult<TValue> operator +(Result left, NullableResult<TValue> right) => right with { Errors = right.Errors.Union(left.Errors).ToList() };
    public static bool operator ==(NullableResult<TValue> left, Result right) => ReferenceEquals(right, Result.Success) && left.IsSuccess;
    public static bool operator !=(NullableResult<TValue> left, Result right) => !ReferenceEquals(right, Result.Success) || !left.IsSuccess;

    public virtual bool Equals(NullableResult<TValue>? other)
        => other is not null
           && (Value is not null
            ? other.Value is not null && Value.Equals(other.Value) && Errors.SequenceEqual(other.Errors)
            : other.Value is null && Errors.SequenceEqual(other.Errors));

    public override int GetHashCode()
        => HashCode.Combine(base.GetHashCode(), Value);
}