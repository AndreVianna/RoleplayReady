namespace System.Validations.Abstractions;

public enum ValidationMode {
    None,
    And,
    Or,
    Not,
}

public interface IValidations {
    string Source { get; }
    List<ValidationError> Errors { get; }
    ValidationMode Mode { get; }
}

public interface IValidations<out TSubject, out TValidations> : IValidations
    where TValidations : Validations<TSubject, TValidations> {
    TSubject? Subject { get; }

    IValidationsConnector<TSubject, TValidations> Not(Func<TSubject?, ValidationResult> validateRight);
    TValidations Not();
}