namespace System.Validations.Abstractions;

public interface INullableDateTimeValidation
    : IFinishesValidation, 
      IConnectsToValidation<IDateTimeValidation> {
    IDateTimeValidation NotNull();
}