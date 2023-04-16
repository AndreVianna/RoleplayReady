namespace System.Validations;

public abstract class Validation<TSubject, TValidation, TChecks>
    : IValidation<TValidation, TChecks>
    where TValidation : class, IValidation<TValidation, TChecks>
    where TChecks : class, IValidations {

    protected Validation(TSubject? subject, string? source, ICollection<ValidationError>? previousErrors = null) {
        Subject = subject;
        Source = source!;
        Errors = new List<ValidationError>(previousErrors ?? Array.Empty<ValidationError>());
    }

    protected TSubject? Subject { get; }
    protected string Source { get; }
    protected ICollection<ValidationError> Errors { get; }

    public TChecks And => (this as TChecks)!;

    public ValidationResult Result => Errors.Any() ? new(Errors.ToArray()) : new();
}