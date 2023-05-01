namespace System.Results;

public record ValidationResult : IResult {
    public ValidationResult(IEnumerable<ValidationError>? errors = null) {
        Errors = errors?.ToList() ?? new List<ValidationError>();
    }

    public bool IsValid => Errors.Count == 0;
    public virtual bool IsSuccess => IsValid;
    public bool HasErrors => Errors.Count != 0;
    public IList<ValidationError> Errors { get; protected init; } = new List<ValidationError>();

    public static ValidationResult AsSuccess()  => new();

    public static ValidationResult AsInvalid(IEnumerable<ValidationError> errors)
        => new(Ensure.IsNotNull(errors));
    public CRUDResult<TOutput> ToCRUDResult<TOutput>(TOutput value) {
        if (!HasErrors) throw new InvalidOperationException("Cannot convert a successful ValidationResult to a CRUDResult.");
        return new(CRUDResultType.Invalid, value, Errors);
    }

    public SignInResult ToSignInResult(string? token = null) {
        if (!HasErrors) throw new InvalidOperationException("Cannot convert a successful ValidationResult to a SignInResult.");
        return new(SignInResultType.Invalid, token, Errors);
    }

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