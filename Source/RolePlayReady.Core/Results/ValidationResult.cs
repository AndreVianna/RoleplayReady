namespace System.Results;

public record ValidationResult : IResult {
    public ValidationResult(IEnumerable<ValidationError>? errors = null) {
        Errors = errors?.ToList() ?? new List<ValidationError>();
    }

    public bool IsValid => Errors.Count == 0;
    public virtual bool IsSuccess => IsValid;
    public bool HasValidationErrors => Errors.Count != 0;
    public IList<ValidationError> Errors { get; protected init; } = new List<ValidationError>();

    public static ValidationResult AsSuccess()  => new();

    public static ValidationResult AsInvalid(IEnumerable<ValidationError> errors)
        => new(Ensure.IsNotNull(errors));
    public CRUDResult<TOutput> ToCRUDResult<TOutput>(TOutput value)
        => HasValidationErrors
            ? new(CRUDResultType.Invalid, value, Errors)
            : throw new InvalidOperationException("Cannot convert a successful ValidationResult to a CrudResult.");

    public SignInResult ToSignInResult()
        => HasValidationErrors
            ? new(SignInResultType.Invalid, null, Errors)
            : throw new InvalidOperationException("Cannot convert a successful ValidationResult to a SignInResult.");

    public static implicit operator ValidationResult(List<ValidationError> errors) => new(errors.AsEnumerable());
    public static implicit operator ValidationResult(ValidationError[] errors) => new(errors.AsEnumerable());
    public static implicit operator ValidationResult(ValidationError error) => new(new[] { error }.AsEnumerable());
    public static implicit operator bool(ValidationResult result) => result.IsSuccess;

    public static ValidationResult operator +(ValidationResult left, ValidationResult right) => new(right.Errors.Union(left.Errors));

    public virtual bool Equals(ValidationResult? other)
        => other is not null
        && Errors.SequenceEqual(other.Errors);

    public override int GetHashCode()
        => Errors.Aggregate(Array.Empty<ValidationError>().GetHashCode(), HashCode.Combine);
}