namespace System.Validations.Abstractions;

public interface IValidatableValidations : IValidations<IValidatable?, ValidatableValidations> {
    IValidationsConnector<IValidatable?, IValidations> IsValid();
}