namespace System.Validation.Commands;

public abstract class ValidationCommand : IValidationCommand {
    protected ValidationCommand(string source, ValidationResult? validation = null) {
        Source = source;
        Validation = validation ?? ValidationResult.Success();
    }

    protected string Source { get; }
    protected ValidationResult Validation { get; set; }

    public virtual ValidationResult Validate(object? subject)
        => subject is not null && !ValidateAs(subject)
            ? AddError(ValidationErrorMessage, GetErrorMessageArguments(subject))
            : Validation;

    public virtual ValidationResult Negate(object? subject)
        => subject is not null && ValidateAs(subject)
            ? AddError(InvertMessage(ValidationErrorMessage), GetErrorMessageArguments(subject))
            : Validation;

    protected Func<object?, bool> ValidateAs { get; init; } = _ => true;
    protected string ValidationErrorMessage { get; init; } = MustBeValid;
    protected Func<object?, object?[]> GetErrorMessageArguments { get; init; } = _ => Array.Empty<object?>();
    protected ValidationResult AddError(string message, params object?[] args)
        => AddError(new ValidationError(message, Source, args));
    protected ValidationResult AddError(ValidationError error)
        => Validation += error;
}