namespace System.Results;

public record CrudResult : Result {
    public CrudResult(CRUDResultType type, IEnumerable<ValidationError>? errors = null)
        : base(errors) {
        Type = IsInvalid ? CRUDResultType.Invalid : type;
    }

    public CRUDResultType Type { get; }
    public override bool IsSuccess => !IsInvalid && Type is CRUDResultType.Success;
    public bool IsNotFound => !IsInvalid ? Type is CRUDResultType.NotFound : throw new InvalidOperationException("The result has validation errors. You must check for errors before checking if result is null.");
    public bool IsConflict => !IsInvalid ? Type is CRUDResultType.Conflict : throw new InvalidOperationException("The result has validation errors. You must check for errors before checking if result has conflicts.");

    public static CrudResult Success { get; } = new(CRUDResultType.Success);
    public static CrudResult NotFound { get; } = new(CRUDResultType.NotFound);
    public static CrudResult Conflict { get; } = new(CRUDResultType.Conflict);
    public static CrudResult Invalid(string message, string source) => Invalid(new ValidationError(message, source));
    public static CrudResult Invalid(ValidationError error) => Invalid(new[] { error });
    public static CrudResult Invalid(IEnumerable<ValidationError> errors) => new(CRUDResultType.Invalid, Ensure.IsNotNullOrEmpty(errors));

    public static CrudResult<TValue> SuccessFor<TValue>(TValue value) => new(CRUDResultType.Success, value);
    public static CrudResult<TValue> NotFoundFor<TValue>(TValue? value = default) => new(CRUDResultType.NotFound, value);
    public static CrudResult<TValue> ConflictFor<TValue>(TValue value) => new(CRUDResultType.Conflict, value);
    public static CrudResult<TValue> InvalidFor<TValue>(TValue value, string message, string source) => InvalidFor(value, new ValidationError(message, source));
    public static CrudResult<TValue> InvalidFor<TValue>(TValue value, ValidationError error) => InvalidFor(value, new[] { error });
    public static CrudResult<TValue> InvalidFor<TValue>(TValue value, IEnumerable<ValidationError> errors) => new(CRUDResultType.Invalid, value, Ensure.IsNotNullOrEmpty(errors));

    public static implicit operator CrudResult(List<ValidationError> errors) => new(CRUDResultType.Invalid, Ensure.IsNotNullOrEmpty(errors));
    public static implicit operator CrudResult(ValidationError[] errors) => new(CRUDResultType.Invalid, Ensure.IsNotNullOrEmpty(errors));
    public static implicit operator CrudResult(ValidationError error) => new(CRUDResultType.Invalid, new[] { error }.AsEnumerable());
    public static implicit operator bool(CrudResult result) => result.IsSuccess;

    public static CrudResult operator +(CrudResult left, ValidationResult right) {
        var errors = left.Errors.Union(right.Errors).ToList();
        return new(errors.Any() ? CRUDResultType.Invalid : left.Type, errors);
    }
}

public record CrudResult<TResult> : CrudResult {
    public CrudResult(CRUDResultType type, TResult? value = default, IEnumerable<ValidationError>? errors = null)
        : base(type, errors) {
        Value = type != CRUDResultType.NotFound ? Ensure.IsNotNull(value) : value;
    }

    public TResult? Value { get; }

    public static implicit operator CrudResult<TResult>(ValidationResult result)
        => !result.IsSuccess
            ? new(CRUDResultType.Invalid, value, Errors)
            : throw new InvalidOperationException("Cannot convert a successful validation to an invalid CRUD result.");
    public static implicit operator CrudResult<TResult>(TResult? value) => new(CRUDResultType.Success, value);
    public static implicit operator TResult?(CrudResult<TResult> input)
        => input.IsSuccess ? input.Value : throw new InvalidCastException($"Cannot assign a failed result to a Value of type {typeof(TResult)}.");

    public static CrudResult<TResult> operator +(CrudResult<TResult> left, ValidationResult right) {
        var errors = left.Errors.Union(right.Errors).ToList();
        return new(errors.Any() ? CRUDResultType.Invalid : left.Type, left.Value, errors);
    }

    public CrudResult<TOutput> MapTo<TOutput>(Func<TResult?, TOutput?> map)
        => new(Type, map(Value), Errors);
}

