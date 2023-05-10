namespace System.Results;

public record ValidationResult : Result {
    public ValidationResult(IEnumerable<ValidationError>? errors = null)
        : base(errors) {
    }

    public override bool IsSuccess => !IsInvalid;
    public static ValidationResult Success() => new();

    public static ValidationResult Invalid(string message, string source, params object?[] args) => Invalid(new ValidationError(message, source, args));
    public static ValidationResult Invalid(ValidationError error) => Invalid(new[] { error });
    public static ValidationResult Invalid(IEnumerable<ValidationError> errors) => new(Ensure.IsNotNull(errors));

    public static implicit operator ValidationResult(List<ValidationError> errors) => new(errors.AsEnumerable());
    public static implicit operator ValidationResult(ValidationError[] errors) => new(errors.AsEnumerable());
    public static implicit operator ValidationResult(ValidationError error) => new(new[] { error }.AsEnumerable());

    public static ValidationResult operator +(ValidationResult left, ValidationResult right) {
        left.Errors.MergeWith(right.Errors.Distinct());
        return left;
    }

    public virtual bool Equals(ValidationResult? other) => base.Equals(other);

    public override int GetHashCode() => base.GetHashCode();
}
