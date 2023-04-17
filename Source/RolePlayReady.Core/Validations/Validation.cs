using static System.Results.ValidationResult;

namespace System.Validations;

public static class Validation {
    public static IEnumerable<ValidationError> EnsureNotNull(object? subject, string? source)
        => subject is null
            ? new[] { new ValidationError(CannotBeNull, source!) }
            : Array.Empty<ValidationError>();
}

public abstract class Validation<TSubject, TValidation>
    : IValidation<TSubject>,
      IConnectsTo<TValidation>
    where TValidation : class, IFinishesValidation {

    protected Validation(TSubject? subject, string? source, IEnumerable<ValidationError>? previousErrors = null) {
        Subject = subject;
        Source = source!;
        Errors = new List<ValidationError>(previousErrors ?? Array.Empty<ValidationError>());
    }

    public TSubject? Subject { get; }
    public string Source { get; }
    public ICollection<ValidationError> Errors { get; }

    public TValidation And => (this as TValidation)!;

    public ValidationResult Result => Success + Errors.ToArray();
}