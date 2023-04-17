namespace System.Validations.Abstractions;

public interface IStringValidation
    : IConnectsToOrFinishes<IStringValidation> {
    IConnectsToOrFinishes<IStringValidation> NotEmptyOrWhiteSpace();
    IConnectsToOrFinishes<IStringValidation> NotShorterThan(int minimumLength);
    IConnectsToOrFinishes<IStringValidation> NotLongerThan(int maximumLength);
}