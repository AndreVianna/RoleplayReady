namespace System.Validations;

public abstract class Validation<TSubject, TValidation>
    : IValidation<TSubject>,
      IConnectsToValidation<TValidation>
    where TValidation : class, IFinishesValidation {

    protected Validation(TSubject? subject, string? source, ICollection<ValidationError>? previousErrors = null) {
        Subject = subject;
        Source = source!;
        Errors = new List<ValidationError>(previousErrors ?? Array.Empty<ValidationError>());
    }

    public TSubject? Subject { get; }
    public string Source { get; }
    public ICollection<ValidationError> Errors { get; }

    public TValidation And => (this as TValidation)!;

    public ValidationResult Result => Errors.Any() ? new(Errors.ToArray()) : new();
}