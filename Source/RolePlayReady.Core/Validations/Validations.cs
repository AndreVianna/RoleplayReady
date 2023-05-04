namespace System.Validations;

public abstract class Validations<TSubject, TValidations> : IValidations<TSubject, TValidations>
    where TValidations : Validations<TSubject, TValidations>
    {
    public static IEnumerable<ValidationError> EnsureNotNull(object? subject, string source)
        => subject is null
            ? new[] { new ValidationError(CannotBeNull, source) }
            : Array.Empty<ValidationError>();

    protected Validations(ValidationMode mode, TSubject? subject, string source, IEnumerable<ValidationError>? previousErrors = null) {
        Mode = mode;
        Subject = subject;
        Source = source;
        Errors = previousErrors?.ToList() ?? new List<ValidationError>();
    }

    public ValidationMode Mode { get; }
    public TSubject? Subject { get; }
    public string Source { get; }
    public List<ValidationError> Errors { get; }
    public ValidationsConnector<TSubject, TValidations> Connector { get; protected init; } = null!;

    public IValidationsConnector<TSubject, TValidations> Not(Func<TSubject?, ValidationResult> validateRight)
        => throw new NotImplementedException();
    public TValidations Not() => throw new NotImplementedException();
}
