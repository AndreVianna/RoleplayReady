using static System.Results.ValidationResult;

namespace System.Validations;

public static class Validation {
    public static IEnumerable<ValidationError> EnsureNotNull(object? subject, string? source)
        => subject is null
            ? new[] { new ValidationError(CannotBeNull, source!) }
            : Array.Empty<ValidationError>();
}

public abstract class Validation<TSubject, TValidators>
    : IValidation<TSubject>,
      IConnectsTo<TValidators>
    where TValidators : class {
    protected Validation(TSubject? subject, string? source, IEnumerable<ValidationError>? previousErrors = null) {
        Subject = subject;
        Source = source!;
        Errors = new List<ValidationError>(previousErrors ?? Array.Empty<ValidationError>());
    }

    public TSubject? Subject { get; }
    public string Source { get; }
    public ICollection<ValidationError> Errors { get; }

    public TValidators And => (this as TValidators)!;

    public ValidationResult Result => Success + Errors.ToArray();
}