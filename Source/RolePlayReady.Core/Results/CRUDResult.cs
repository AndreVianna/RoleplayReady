using static System.Utilities.Ensure;

namespace System.Results;

public record CrudResult : Result {
    public CrudResult(CRUDResultType type, IEnumerable<ValidationError>? errors = null)
        : base(errors) {
        Type = IsInvalid ? CRUDResultType.Invalid : type;
    }

    public CRUDResultType Type { get; protected set; }
    public override bool IsSuccess => !IsInvalid && Type is CRUDResultType.Success;
    public bool IsNotFound => !IsInvalid
        ? Type is CRUDResultType.NotFound
        : throw new InvalidOperationException("The result has validation errors. You must check for errors before checking if result is null.");
    public bool IsConflict => !IsInvalid
        ? Type is CRUDResultType.Conflict
        : throw new InvalidOperationException("The result has validation errors. You must check for errors before checking if result has conflicts.");

    public static CrudResult Success() => new(CRUDResultType.Success);

    public static CrudResult NotFound() => new(CRUDResultType.NotFound);

    public static CrudResult Conflict() => new(CRUDResultType.Conflict);

    public static CrudResult Invalid(string message, string source, params object?[] args) => Invalid(new ValidationError(message, source, args));

    public static CrudResult Invalid(ValidationResult result) => new(CRUDResultType.Invalid, result.Errors);

    public static CrudResult<TValue> Success<TValue>(TValue value) => new(CRUDResultType.Success, value);

    public static CrudResult<TValue> NotFound<TValue>(TValue? value = default) => new(CRUDResultType.NotFound, value);

    public static CrudResult<TValue> Conflict<TValue>(TValue value) => new(CRUDResultType.Conflict, value);

    public static CrudResult<TValue> Invalid<TValue>(TValue value, string message, string source) => Invalid(value, new ValidationError(message, source));

    public static CrudResult<TValue> Invalid<TValue>(TValue value, ValidationError error) => Invalid(value, [error]);

    public static CrudResult<TValue> Invalid<TValue>(TValue value, IEnumerable<ValidationError> errors) => new(CRUDResultType.Invalid, value, IsNotNullAndDoesNotHaveNull(errors));

    public static implicit operator CrudResult(List<ValidationError> errors) => new(CRUDResultType.Invalid, IsNotNullAndDoesNotHaveNull(errors));
    public static implicit operator CrudResult(ValidationError[] errors) => new(CRUDResultType.Invalid, IsNotNullAndDoesNotHaveNull(errors));
    public static implicit operator CrudResult(ValidationError error) => new(CRUDResultType.Invalid, new[] { error }.AsEnumerable());

    public static CrudResult operator +(CrudResult left, ValidationResult right) {
        left.Errors.MergeWith(right.Errors.Distinct());
        left.Type = left.IsInvalid ? CRUDResultType.Invalid : left.Type;
        return left;
    }
}

public record CrudResult<TResult> : CrudResult {
    public CrudResult(CRUDResultType type, TResult? value = default, IEnumerable<ValidationError>? errors = null)
        : base(type, errors) {
        Value = type != CRUDResultType.NotFound ? IsNotNull(value) : value;
    }

    public TResult? Value { get; }

    public static implicit operator CrudResult<TResult>(TResult? value) => new(CRUDResultType.Success, value);

    public static CrudResult<TResult> operator +(CrudResult<TResult> left, ValidationResult right) {
        left.Errors.MergeWith(right.Errors.Distinct());
        left.Type = left.IsInvalid ? CRUDResultType.Invalid : left.Type;
        return left;
    }

    public CrudResult<TOutput> MapTo<TOutput>(Func<TResult?, TOutput?> map) => new(Type, map(Value), Errors);
}
