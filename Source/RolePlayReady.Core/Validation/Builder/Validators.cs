using System.Validation.Commands;

namespace System.Validation.Builder;

public abstract class Validators<TSubject, TValidators> : IValidators<TSubject, TValidators>
    where TValidators : Validators<TSubject, TValidators> {
    public static ValidationResult EnsureNotNull(object? subject, string source)
        => subject is null
            ? new[] { new ValidationError(InvertMessage(MustBeNull), source) }
            : ValidationResult.Success();

    protected Validators(ValidatorMode mode, TSubject? subject, string source, ValidationResult? previousResult = null) {
        Mode = mode;
        Subject = subject;
        Source = source;
        Result = previousResult ?? ValidationResult.Success();
        CommandFactory = ValidationCommandFactory.For(Subject, Source, Result);
    }

    public ValidationCommandFactory<TSubject> CommandFactory { get; set; }

    public ValidatorMode Mode { get; }
    public TSubject? Subject { get; }
    public string Source { get; }
    public ValidationResult Result { get; }
    public Connectors<TSubject, TValidators> Connector { get; protected init; } = null!;

    public IConnectors<TSubject, TValidators> Not(Func<TSubject?, ValidationResult> validateRight)
        => throw new NotImplementedException();
    public TValidators Not() => throw new NotImplementedException();
}