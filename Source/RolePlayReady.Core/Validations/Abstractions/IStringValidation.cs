namespace System.Validations.Abstractions;

public interface IStringValidation
    : IFinishesValidation, 
      IConnectsToValidation<IStringValidation> {
    IStringValidation NotNull();
    IStringValidation NotEmptyOrWhiteSpace();
    IStringValidation NotShorterThan(int minimumLength);
    IStringValidation NotLongerThan(int maximumLength);
}