namespace System.Validation.Builder.Abstractions;

public interface IConnectors<out TSubject, out TValidators>
    where TValidators : IValidators {
    TValidators And();
    TValidators Or();
    TValidators And(Func<TSubject?, ValidationResult> validateRight);
    TValidators Or(Func<TSubject?, ValidationResult> validateRight);
    ValidationResult Result { get; }
}