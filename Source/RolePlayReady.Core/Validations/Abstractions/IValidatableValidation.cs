namespace System.Validations.Abstractions;

public interface IValidatableValidation
    : IConnectsToOrFinishes<IValidatableValidators>,
        IValidatableValidators {
}

public interface IValidatableValidators {
    IFinishesValidation IsValid();
}