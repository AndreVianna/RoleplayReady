namespace System.Validations.Abstractions;

public interface IStringValidation
    : IConnectsToOrFinishes<IStringValidators>,
        IStringValidators {
}

public interface IStringValidators {
    IConnectsToOrFinishes<IStringValidators> IsNotEmptyOrWhiteSpace();
    IConnectsToOrFinishes<IStringValidators> MinimumLengthIs(int length);
    IConnectsToOrFinishes<IStringValidators> MaximumLengthIs(int length);
    IConnectsToOrFinishes<IStringValidators> LengthIs(int length);
}