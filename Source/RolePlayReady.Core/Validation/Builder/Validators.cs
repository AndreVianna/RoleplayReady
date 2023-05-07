namespace System.Validation.Builder;

public abstract class Validators : IValidators {
    protected Validators(ValidatorMode mode, string source, ValidationResult? previousResult = null) {
        Mode = mode;
        Source = source;
        Result = previousResult ?? ValidationResult.Success();
    }

    public ValidatorMode Mode { get; }
    public string Source { get; }
    public ValidationResult Result { get; }
}

public abstract class Validators<TSubject> : Validators {
    protected Validators(ValidatorMode mode, TSubject? subject, string source, ValidationResult? previousResult = null)
        : base(mode, source, previousResult) {
        Subject = subject;
    }

    public TSubject? Subject { get; }
}