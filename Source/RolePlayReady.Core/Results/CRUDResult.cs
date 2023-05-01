using static System.Results.CRUDResultType;

namespace System.Results;

public record CRUDResult : ValidationResult {
    public CRUDResult(CRUDResultType type, IEnumerable<ValidationError>? errors = null)
        : base(errors) {
        Type = HasErrors ? Invalid : type;
    }

    public CRUDResultType Type { get; }
    public override bool IsSuccess => !HasErrors && Type is Succeeded;
    public bool IsNotFound => !HasErrors ? Type is NotFound : throw new InvalidOperationException("The result has validation errors. You must check for errors before checking if result is null.");
    public bool IsConflict => !HasErrors ? Type is Conflict : throw new InvalidOperationException("The result has validation errors. You must check for errors before checking if result has conflicts.");

    public static new CRUDResult AsSuccess() => new(Succeeded);
    public static CRUDResult AsNotFound() => new(NotFound);
    public static CRUDResult AsConflict() => new(Conflict);
    public static new CRUDResult<TValue> AsSuccessFor<TValue>(TValue value) => new(Succeeded, value);
    public static CRUDResult<TValue> AsNotFoundFor<TValue>(TValue? value = default) => new(NotFound, value);
    public static CRUDResult<TValue> AsConflictFor<TValue>(TValue value) => new(Conflict, value);

    public static new CRUDResult<TValue> AsInvalidFor<TValue>(TValue value, string message, string source)
        => AsInvalidFor(value, new ValidationError(message, source));
    public static new CRUDResult<TValue> AsInvalidFor<TValue>(TValue value, ValidationError error)
        => AsInvalidFor(value, new[] { error });
    public static new CRUDResult<TValue> AsInvalidFor<TValue>(TValue value, IEnumerable<ValidationError> errors)
        => new(Invalid, value, Ensure.IsNotNullOrEmpty(errors));

    public static implicit operator CRUDResult(List<ValidationError> errors)
        => new(Invalid, Ensure.IsNotNullOrEmpty(errors));
    public static implicit operator CRUDResult(ValidationError[] errors)
        => new(Invalid, Ensure.IsNotNullOrEmpty(errors));
    public static implicit operator CRUDResult(ValidationError error)
        => new(Invalid, new[] { error }.AsEnumerable());

    public static CRUDResult operator +(CRUDResult left, ValidationResult right) {
        var errors = left.Errors.Union(right.Errors).ToList();
        return new(errors.Any() ? Invalid : left.Type, errors);
    }

    public static CRUDResult operator +(ValidationResult left, CRUDResult right) {
        var errors = left.Errors.Union(right.Errors).ToList();
        return new(errors.Any() ? Invalid : right.Type, errors);
    }
}

public record CRUDResult<TValue> : CRUDResult {
    public CRUDResult(CRUDResultType type, TValue? value = default, IEnumerable<ValidationError>? errors = null)
        : base(type, errors) {
        Value = type != NotFound ? Ensure.IsNotNull(value) : value;
    }

    public TValue? Value { get; }

    public static implicit operator CRUDResult<TValue>(TValue? value) => new(Succeeded, value);
    public static implicit operator TValue?(CRUDResult<TValue> input)
        => input.IsSuccess ? input.Value : throw new InvalidCastException($"Cannot assign a failed result to a Value of type {typeof(TValue)}.");

    public static CRUDResult<TValue> operator +(CRUDResult<TValue> left, ValidationResult right) {
        var errors = left.Errors.Union(right.Errors).ToList();
        return new(errors.Any() ? Invalid : left.Type, left.Value, errors);
    }

    public static CRUDResult<TValue> operator +(ValidationResult left, CRUDResult<TValue> right) {
        var errors = left.Errors.Union(right.Errors).ToList();
        return new(errors.Any() ? Invalid : right.Type, right.Value, errors);
    }

    public CRUDResult<TOutput> Map<TOutput>(Func<TValue?, TOutput?> map)
        => new(Type, map(Value), Errors);
}
