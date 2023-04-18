namespace System.Validations.Abstractions;

public interface IStringValidation
    : IConnectsToOrFinishes<IStringValidation> {
    IConnectsToOrFinishes<IStringValidation> NotEmptyOrWhiteSpace();
    IConnectsToOrFinishes<IStringValidation> NoShorterThan(int length);
    IConnectsToOrFinishes<IStringValidation> NoLongerThan(int length);
    IConnectsToOrFinishes<IStringValidation> Exactly(int length);
}