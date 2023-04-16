namespace System.Validations.Abstractions;

public interface IReferenceTypeValidation
    : IFinishesValidation,
      IConnectsToValidation<IReferenceTypeValidation> {
    IReferenceTypeValidation NotNull();
}