namespace System.Results;

public record SearchResult : Result {
    public SearchResult(bool found = false)
        : this(found, Array.Empty<ValidationError>()) { }

    protected SearchResult(bool found, IEnumerable<ValidationError>? errors = null)
        : base(errors) {
        IsNotFound = !found;
    }

    public override bool IsSuccess => base.IsSuccess && !IsNotFound;
    public bool IsNotFound { get; }

    public static new SearchResult Success { get; } = new(true);
    public static new SearchResult<TValue> FromValue<TValue>(TValue? value) => new(value, true);

    public static new SearchResult Failure(string message, string source) => Failure(new ValidationError(message, source));
    public static new SearchResult Failure(ValidationError error) => Failure(new[] { error });
    public static new SearchResult Failure(IEnumerable<ValidationError> errors) => new(false, Ensure.IsNotNullOrEmpty(errors));
    public static new SearchResult<TValue> Failure<TValue>(TValue value, string message, string source) => Failure(value, new ValidationError(message, source));
    public static new SearchResult<TValue> Failure<TValue>(TValue value, ValidationError error) => Failure(value, new[] { error });
    public static new SearchResult<TValue> Failure<TValue>(TValue value, IEnumerable<ValidationError> errors) => new(value, value is not null, errors);

    public static SearchResult NotFound() => new();
    public static SearchResult<TValue> NotFound<TValue>(TValue value) => new(value);

    public static implicit operator SearchResult(List<ValidationError> errors) => new(false, errors.AsEnumerable());
    public static implicit operator SearchResult(ValidationError[] errors) => new(false, errors.AsEnumerable());
    public static implicit operator SearchResult(ValidationError error) => new(false, new[] { error }.AsEnumerable());
    public static implicit operator bool(SearchResult result) => result.IsSuccess;

    public static SearchResult operator +(SearchResult left, Result right) => new(!left.IsNotFound, right.Errors.Union(left.Errors));

    public virtual bool Equals(SearchResult? other)
        => other is not null
        && IsNotFound == other.IsNotFound
        && Errors.SequenceEqual(other.Errors);

    public override int GetHashCode()
        => HashCode.Combine(base.GetHashCode(), IsNotFound);
}

public record SearchResult<TValue> : SearchResult {
    public SearchResult(TValue? input = default, bool found = false) 
        : this((object?)input, found) { }

    public SearchResult(object? value, bool found = false, IEnumerable<ValidationError>? errors = null)
        : base(found, errors) {
        Value = value switch {
            TValue v => v,
            null => default!,
            _ => throw new InvalidCastException(string.Format(CannotAssign, $"Result<{typeof(TValue).GetName()}>", $"Result<{value.GetType().GetName()}>"))
        };
    }

    public TValue Value { get; }

    public static implicit operator SearchResult<TValue>(Result<object?> value) => new(value.Value, value.Value is not null, value.Errors);
    public static implicit operator SearchResult<TValue>(TValue? value) => new(value, value is not null, Array.Empty<ValidationError>());
    public static implicit operator TValue?(SearchResult<TValue> input) => input.IsNotFound ? default : input.Value;

    public static SearchResult<TValue> operator +(SearchResult<TValue> left, Result right) => new(left.Value, !left.IsNotFound, left.Errors.Union(right.Errors));
    public static SearchResult<TValue> operator +(Result left, SearchResult<TValue> right) => new(right.Value, !right.IsNotFound, right.Errors.Union(left.Errors));

    public static bool operator ==(SearchResult<TValue> left, Result right) => ReferenceEquals(right, Result.Success) && left.IsSuccess;
    public static bool operator !=(SearchResult<TValue> left, Result right) => !ReferenceEquals(right, Result.Success) || !left.IsSuccess;

    public virtual bool Equals(SearchResult<TValue>? other)
        => other is not null
        && (Value?.Equals(other.Value) ?? other.Value is null)
        && (IsNotFound == other.IsNotFound)
        && Errors.SequenceEqual(other.Errors);

    public override int GetHashCode()
        => HashCode.Combine(base.GetHashCode(), Value);
}
