namespace System.Validation.Builder;

public abstract class Validator : IValidator {
    protected Validator(string source, ValidatorMode mode = ValidatorMode.And) {
        Mode = mode;
        Source = source;
        Result = ValidationResult.Success();
    }

    public ValidatorMode Mode { get; }
    public string Source { get; }
    public ValidationResult Result { get; protected set; }
}

public abstract class Validator<TSubject> : Validator {
    protected Validator(TSubject? subject, string source, ValidatorMode mode = ValidatorMode.And)
        : base(source, mode) {
        Subject = subject;
    }

    public TSubject? Subject { get; }
}