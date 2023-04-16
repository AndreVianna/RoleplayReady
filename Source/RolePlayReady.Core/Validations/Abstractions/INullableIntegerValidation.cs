namespace System.Validations.Abstractions;

public interface INullableIntegerValidation
    : IFinishesValidation, 
      IConnectsToValidation<IIntegerValidation> {
    IIntegerValidation NotNull();
}