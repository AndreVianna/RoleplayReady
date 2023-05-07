namespace System.Validation.Commands;

public abstract class ValidationCommand<TSubject> : IValidationCommand {
    protected ValidationCommand(/*TSubject? subject, */string source, ValidationResult? validation = null) {
//        Subject = subject;
        Source = source;
        Validation = validation ?? ValidationResult.Success();
    }

    //protected TSubject? Subject { get; }
    protected string Source { get; }
    protected ValidationResult Validation { get; set; }

    ValidationResult IValidationCommand.Validate(object? subject)
        => subject is not null
            ? Validate((TSubject)subject)
            : Validation;

    public virtual ValidationResult Validate(TSubject subject)
        => !ValidateAs(subject)
            ? AddError(ValidationErrorMessage, Arguments)
            : Validation;

    ValidationResult IValidationCommand.Negate(object? subject)
        => subject is not null
            ? Negate((TSubject)subject)
            : Validation;

    public virtual ValidationResult Negate(TSubject subject)
        => !NegateAs?.Invoke(subject) ?? ValidateAs(subject)
            ? AddError(NegationErrorMessage ?? InvertMessage(ValidationErrorMessage), Arguments)
            : Validation;

    protected Func<TSubject, bool> ValidateAs { get; init; } = _ => true;
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