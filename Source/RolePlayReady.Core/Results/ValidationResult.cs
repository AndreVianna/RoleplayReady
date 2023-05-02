namespace System.Results;

public record ValidationResult : Result {
    public ValidationResult(IEnumerable<ValidationError>? errors = null)
        : base(errors) {
    }

    public override bool IsSuccess => !IsInvalid;
    public static ValidationResult Success { get; } = new();

    public static ValidationResult AsInvalid(IEnumerable<ValidationError> errors)
        => new(Ensure.IsNotNull(errors));

    public CrudResult<TOutput> ToInvalidCrudResult<TOutput>(TOutput value)
        => !IsSuccess
            ? new(CRUDResultType.Invalid, value, Errors)
            : throw new InvalidOperationException("Cannot convert a successful validation to an invalid CRUD result.");

    public SignInResult ToInvalidSignInResult()
        => !IsSuccess
            ? new(SignInResultType.Invalid, null, Errors)
            : throw new InvalidOperationException("Cannot convert a successful validation to an invalid sign in result.");

    public static implicit operator ValidationResult(List<ValidationError> errors) => new(errors.AsEnumerable());
    public static implicit operator ValidationResult(ValidationError[] errors) => new(errors.AsEnumerable());
    public static implicit operator ValidationResult(ValidationError error) => new(new[] { error }.AsEnumerable());
    public static implicit operator bool(ValidationResult result) => result.IsSuccess;

    public static ValidationResult operator +(ValidationResult left, ValidationResult right) => new(right.Errors.Union(left.Errors));

    public virtual bool Equals(ValidationResult? other) => base.Equals(other);

    public override int GetHashCode() => base.GetHashCode();
}
