namespace System.Validation.Abstractions;

public interface IValidatableValidators : IValidators<IValidatable?, ValidatableValidators> {
    IValidatorsConnector<IValidatable?, IValidators> IsValid();
}