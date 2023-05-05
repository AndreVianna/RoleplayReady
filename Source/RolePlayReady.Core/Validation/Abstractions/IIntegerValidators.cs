namespace System.Validation.Abstractions;

public interface IIntegerValidators : IValidators<int?, IntegerValidators> {
    IValidatorsConnector<int?, IntegerValidators> MinimumIs(int minimum);
    IValidatorsConnector<int?, IntegerValidators> IsGreaterThan(int minimum);
    IValidatorsConnector<int?, IntegerValidators> IsEqualTo(int value);
    IValidatorsConnector<int?, IntegerValidators> IsLessThan(int maximum);
    IValidatorsConnector<int?, IntegerValidators> MaximumIs(int maximum);
}