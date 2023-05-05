namespace System.Validation.Builder.Abstractions;

public interface IValidatableValidators : IValidators<IValidatable?, ValidatableValidators> {
    IConnectors<IValidatable?, IValidators> IsValid();
}