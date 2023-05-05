namespace System.Validation.Commands;

public abstract class ValidationCommand<TSubject> : IValidationCommand {
    protected ValidationCommand(TSubject subject, string source, ValidationResult? validation = null) {
        Subject = subject;
        Source = source;
        Validation = validation ?? ValidationResult.Success();
    }

    public TSubject Subject { get; }
    public string Source { get; }
    public ValidationResult Validation { get; protected set; }

    public abstract ValidationResult Validate();
    protected ValidationResult AddError(string message, params object?[] args)
        => AddError(new ValidationError(message, Source, args));

    protected ValidationResult AddError(ValidationError error)
        => Validation += error;
}