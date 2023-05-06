namespace System.Validation.Builder;

public abstract class Validators<TSubject> : IValidators {
    public static ValidationResult EnsureNotNull(object? subject, string source)
        => subject is null
            ? new[] { new ValidationError(InvertMessage(MustBeNull), source) }
            : ValidationResult.Success();

    protected Validators(ValidatorMode mode, TSubject? subject, string source, ValidationResult? previousResult = null) {
        Mode = mode;
        Subject = subject;
        Source = source;
        Result = previousResult ?? ValidationResult.Success();
    }

    public ValidatorMode Mode { get; }
    public TSubject? Subject { get; }
    public string Source { get; }
    public ValidationResult Result { get; }
}