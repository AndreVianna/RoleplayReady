namespace System.Validations.Abstractions;

public interface INumberValidation<in TValue>
    : IConnectsToOrFinishes<INumberValidators<TValue>>,
        INumberValidators<TValue> {
}