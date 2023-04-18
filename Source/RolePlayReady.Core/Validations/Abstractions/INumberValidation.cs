namespace System.Validations.Abstractions;

public interface INumberValidation<in TValue>
    : IConnectsToOrFinishes<INumberValidation<TValue>>
    where TValue : IComparable<TValue> {
    IConnectsToOrFinishes<INumberValidation<TValue>> LessOrEqualTo(TValue maximum);
    IConnectsToOrFinishes<INumberValidation<TValue>> GreaterOrEqualTo(TValue minimum);
    IConnectsToOrFinishes<INumberValidation<TValue>> LessThan(TValue maximum);
    IConnectsToOrFinishes<INumberValidation<TValue>> GreaterThan(TValue minimum);
}