namespace System.Validations.Abstractions;

public interface ITypeValidation
    : IConnectsToOrFinishes<ITypeValidators>,
        ITypeValidators {
}