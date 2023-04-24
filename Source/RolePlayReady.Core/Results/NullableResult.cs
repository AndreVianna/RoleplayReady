using System.Extensions;

namespace System.Results;

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

    public bool IsNull => Value is null;
    public bool HasValue => Value is not null;

    public TValue? Value { get; }

    public static implicit operator NullableResult<TValue>(Result<TValue> value) => new(value.Value, value.Errors);
    public static implicit operator NullableResult<TValue>(NullableResult<object> value) => new(value.Value, value.Errors);
    public static implicit operator NullableResult<TValue>(TValue? value) => new(value, Array.Empty<ValidationError>());
    public static implicit operator TValue?(NullableResult<TValue> input) => input.Value;

    public static NullableResult<TValue> operator +(NullableResult<TValue> left, ValidationResult right) => left with { Errors = left.Errors.Union(right.Errors).ToList() };
    public static NullableResult<TValue> operator +(ValidationResult left, NullableResult<TValue> right) => right with { Errors = right.Errors.Union(left.Errors).ToList() };
    public static bool operator ==(NullableResult<TValue> left, ValidationResult right) => ReferenceEquals(right, ValidationResult.Success) && left.IsSuccess;
    public static bool operator !=(NullableResult<TValue> left, ValidationResult right) => !ReferenceEquals(right, ValidationResult.Success) || !left.IsSuccess;

    public virtual bool Equals(NullableResult<TValue>? other)
        => other is not null
           && (Value is not null
            ? other.Value is not null && Value.Equals(other.Value) && Errors.SequenceEqual(other.Errors)
            : other.Value is null && Errors.SequenceEqual(other.Errors));

    public override int GetHashCode()
        => HashCode.Combine(base.GetHashCode(), Value);
}