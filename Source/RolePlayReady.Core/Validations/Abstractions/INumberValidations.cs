namespace System.Validations.Abstractions;

public interface INumberValidations<in TValue> : IValidations<IComparable<TValue>> {
    IConnects<INumberValidations<TValue>> MinimumIs(TValue minimum);
    IConnects<INumberValidations<TValue>> IsGreaterThan(TValue minimum);
    IConnects<INumberValidations<TValue>> IsEqualTo(TValue value);
    IConnects<INumberValidations<TValue>> IsLessThan(TValue maximum);
    IConnects<INumberValidations<TValue>> MaximumIs(TValue maximum);
}