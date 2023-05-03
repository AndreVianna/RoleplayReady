namespace System.Validations.Abstractions;

public interface IObjectValidation
    : IConnectsToOrFinishes<IObjectValidators>,
        IObjectValidators {
}