namespace System.Validation.Abstractions;

public interface IValidatorsConnector<out TSubject, out TValidators>
    where TValidators : IValidators {
    TValidators And();
    TValidators Or();
    TValidators And(Func<TSubject?, ValidationResult> validateRight);
    TValidators Or(Func<TSubject?, ValidationResult> validateRight);
    ValidationResult Result { get; }
}