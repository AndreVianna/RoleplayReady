namespace System.Validations.Abstractions;

public interface IValidatableValidations : IValidations {
    IConnectors<IValidatableValidations> NotNull();
    IConnectors<IReferenceTypeValidations> Valid();
}