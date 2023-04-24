namespace System.Validations.Abstractions;

public interface ITypeValidation
    : IConnectsToOrFinishes<ITypeValidators>,
        ITypeValidators {
}

public interface ITypeValidators {
    IConnectsToOrFinishes<ITypeValidators> IsEqualTo<TType>();
}