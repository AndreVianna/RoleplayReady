namespace System.Results;

public sealed class ValidationResult : ResultBase<SuccessfulResult> {
    public ValidationResult() : base(SuccessfulResult.Success) { }

    public ValidationResult(object? input) : base(input) { }

    public static implicit operator ValidationResult(SuccessfulResult _) => new();
    public static implicit operator ValidationResult(Failure failure) => new(failure.Errors);
    public static implicit operator ValidationResult(List<ValidationError> errors) => new(errors);
    public static implicit operator ValidationResult(ValidationError[] errors) => new(errors);
    public static implicit operator ValidationResult(ValidationError error) => new(error);

    public static ValidationResult operator +(ValidationResult left, ValidationResult right) => (ValidationResult)left.AddErrors(right.Errors);
}