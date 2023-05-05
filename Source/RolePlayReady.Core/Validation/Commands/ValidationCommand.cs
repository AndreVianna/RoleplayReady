using System.Validation.Commands.Abstractions;

namespace System.Validation.Commands;

public abstract class ValidationCommand<TSubject> : IValidationCommand {
    protected ValidationCommand(TSubject subject, string source, ValidationResult? validation = null) {
        Subject = subject;
        Source = source;
        Validation = validation ?? ValidationResult.Success();
    }

    protected TSubject Subject { get; }
    protected string Source { get; }
    protected ValidationResult Validation { get; set; }

    public virtual ValidationResult Validate() 
        => !ValidateAs(Subject)
            ? AddError(ValidationErrorMessage, ValidationArguments)
            : Validation;

    public virtual ValidationResult Negate()
        => !NegateAs?.Invoke(Subject) ?? ValidateAs(Subject)
            ? AddError(NegationErrorMessage ?? InvertMessage(ValidationErrorMessage), NegationArguments ?? ValidationArguments)
            : Validation;

    protected Func<TSubject, bool> ValidateAs { get; init; } = _ => true;
    protected string ValidationErrorMessage { get; init; } = MustBeValid;
    protected object?[] ValidationArguments { get; init; } = Array.Empty<object?>();
    protected Func<TSubject, bool>? NegateAs { get; init; }
    protected string? NegationErrorMessage { get; init; }
    protected object?[]? NegationArguments { get; init; }
    protected object?[] AddArguments(params object?[] args) => args;
    protected ValidationResult AddError(string message, params object?[] args)
        => AddError(new ValidationError(message, Source, args));
    protected ValidationResult AddError(ValidationError error)
        => Validation += error;
}