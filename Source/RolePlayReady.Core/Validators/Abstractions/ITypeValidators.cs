namespace System.Validators.Abstractions;

public interface ITypeValidators {
    IConnectsToOrFinishes<ITypeValidators> IsEqualTo<TType>();
}