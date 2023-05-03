namespace System.Validators.Abstractions;

public interface IObjectValidators {
    IConnectsToOrFinishes<ICollectionValidators<TType>> IsOfType<TType>();
}