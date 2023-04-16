namespace System.Validations.Abstractions;

public interface IValidatableObjectValidations : IValidations {
    IConnectors<IValidatableObjectValidations> NotNull();
    IConnectors<IReferenceTypeValidations> Valid();
}