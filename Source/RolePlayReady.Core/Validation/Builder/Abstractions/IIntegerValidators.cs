namespace System.Validation.Builder.Abstractions;

public interface IIntegerValidators : IValidators<int?, IntegerValidators> {
    IConnectors<int?, IntegerValidators> MinimumIs(int minimum);
    IConnectors<int?, IntegerValidators> IsGreaterThan(int minimum);
    IConnectors<int?, IntegerValidators> IsEqualTo(int value);
    IConnectors<int?, IntegerValidators> IsLessThan(int maximum);
    IConnectors<int?, IntegerValidators> MaximumIs(int maximum);
}