namespace System.Validators.Abstractions;

public interface INumberValidators<in TValue> {
    IConnectsToOrFinishes<INumberValidators<TValue>> MinimumIs(TValue minimum);
    IConnectsToOrFinishes<INumberValidators<TValue>> IsGreaterThan(TValue minimum);
    IConnectsToOrFinishes<INumberValidators<TValue>> IsEqualTo(TValue value);
    IConnectsToOrFinishes<INumberValidators<TValue>> IsLessThan(TValue maximum);
    IConnectsToOrFinishes<INumberValidators<TValue>> MaximumIs(TValue maximum);
}