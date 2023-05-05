namespace System.Validation.Abstractions;

public interface IValidators {
    string Source { get; }
    ValidationResult Result { get; }
    ValidatorMode Mode { get; }
}

public interface IValidators<out TSubject, out TValidators> : IValidators
    where TValidators : Validators<TSubject, TValidators> {
    TSubject? Subject { get; }

    IValidatorsConnector<TSubject, TValidators> Not(Func<TSubject?, ValidationResult> validateRight);
    TValidators Not();
}