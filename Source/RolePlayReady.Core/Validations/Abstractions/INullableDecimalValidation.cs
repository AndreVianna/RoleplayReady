namespace System.Validations.Abstractions;

public interface INullableDecimalValidation
    : IFinishesValidation, 
      IConnectsToValidation<IDecimalValidation> {
    IDecimalValidation NotNull();
}