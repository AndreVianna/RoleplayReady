namespace System.Validation.Builder.Abstractions;

public interface IValidatableValidators : IValidators {
    IConnectors<IValidatable?, IValidators> IsValid();
}