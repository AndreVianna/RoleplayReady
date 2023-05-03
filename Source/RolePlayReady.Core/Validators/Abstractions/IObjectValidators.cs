namespace System.Validators.Abstractions;

public interface IObjectValidators {
    IConnectsToOrFinishes<TType> IsOfType<TType>();
}