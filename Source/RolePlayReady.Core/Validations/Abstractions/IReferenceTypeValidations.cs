namespace System.Validations.Abstractions;

public interface IReferenceTypeValidations : IValidations {
    IConnectors<IReferenceTypeValidations> NotNull();
}