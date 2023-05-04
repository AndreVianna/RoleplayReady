namespace System.Validations.Abstractions;

public interface IDecimalValidations<TValue> : IValidations<IComparable<TValue>, NumberValidations<TValue>>
    where TValue : IComparable<TValue> {
    IValidationsConnector<IComparable<TValue>?, NumberValidations<TValue>> MinimumIs(TValue minimum);
    IValidationsConnector<IComparable<TValue>?, NumberValidations<TValue>> IsGreaterThan(TValue minimum);
    IValidationsConnector<IComparable<TValue>?, NumberValidations<TValue>> IsEqualTo(TValue value);
    IValidationsConnector<IComparable<TValue>?, NumberValidations<TValue>> IsLessThan(TValue maximum);
    IValidationsConnector<IComparable<TValue>?, NumberValidations<TValue>> MaximumIs(TValue maximum);
}