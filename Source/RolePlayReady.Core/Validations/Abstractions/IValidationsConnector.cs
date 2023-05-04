namespace System.Validations.Abstractions;

public interface IValidationsConnector<out TSubject, out TValidations>
    where TValidations : IValidations {
    TValidations And();
    TValidations Or();
    TValidations And(Func<TSubject?, ValidationResult> validateRight);
    TValidations Or(Func<TSubject?, ValidationResult> validateRight);
    ValidationResult Result { get; }
}
