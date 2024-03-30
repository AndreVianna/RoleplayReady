namespace System.Validation.Commands;

public abstract class ValidationCommand(string source) : IValidationCommand {
    protected string Source { get; } = source;

    public virtual ValidationResult Validate(object? subject) => subject is null || ValidateAs(subject)
            ? ValidationResult.Success()
            : ValidationResult.Invalid(ValidationErrorMessage, Source, GetErrorMessageArguments(subject));

    public virtual ValidationResult Negate(object? subject) => subject is null || !ValidateAs(subject)
            ? ValidationResult.Success()
            : ValidationResult.Invalid(InvertMessage(ValidationErrorMessage), Source, GetErrorMessageArguments(subject));

    protected Func<object, bool> ValidateAs { get; init; } = _ => true;
    protected string ValidationErrorMessage { get; init; } = MustBeValid;
    protected Func<object, object?[]> GetErrorMessageArguments { get; init; } = _ => [];
}