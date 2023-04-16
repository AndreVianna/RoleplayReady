namespace System.Validations.Abstractions;

public interface IDateTimeValidation
    : IFinishesValidation,
      IConnectsToValidation<IDateTimeValidation> {
    IDateTimeValidation After(DateTime reference);
    IDateTimeValidation Before(DateTime reference);
    IDateTimeValidation StartsOn(DateTime reference);
    IDateTimeValidation EndsOn(DateTime reference);
}