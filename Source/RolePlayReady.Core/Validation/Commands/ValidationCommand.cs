namespace System.Validation.Commands;

public abstract class ValidationCommand<TSubject> : IValidationCommand {
    protected ValidationCommand(TSubject? subject, string source, ValidationResult? validation = null) {
        Subject = subject;
        Source = source;
        Validation = validation ?? ValidationResult.Success();
    }

    protected TSubject? Subject { get; }
    protected string Source { get; }
    protected ValidationResult Validation { get; set; }

    public virtual ValidationResult Validate() 
        => Subject is not null && !ValidateAs(Subject)
            ? AddError(ValidationErrorMessage, Arguments)
            : Validation;

    public virtual ValidationResult Negate()
        => Subject is not null && (!NegateAs?.Invoke(Subject) ?? ValidateAs(Subject))
            ? AddError(NegationErrorMessage ?? InvertMessage(ValidationErrorMessage), Arguments)
            : Validation;

    protected Func<TSubject, bool> ValidateAs { get; init; } = s => true;
    protected Func<TSubject, bool>? NegateAs { get; init; }
    protected string ValidationErrorMessage { get; init; } = MustBeValid;
    protected string? NegationErrorMessage { get; init; }
    protected object?[] Arguments { get; init; } = Array.Empty<object?>();
    protected object?[] SetArguments(params object?[] args) => args;
    protected ValidationResult AddError(string message, params object?[] args)
        => AddError(new ValidationError(message, Source, args));
    protected ValidationResult AddError(ValidationError error)
        => Validation += error;
}