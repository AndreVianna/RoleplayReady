namespace System.Validation.Builder.Abstractions;

public interface IDecimalValidators : IValidators<decimal?, DecimalValidators> {
    IConnectors<decimal?, DecimalValidators> MinimumIs(decimal minimum);
    IConnectors<decimal?, DecimalValidators> IsGreaterThan(decimal minimum);
    IConnectors<decimal?, DecimalValidators> IsEqualTo(decimal value);
    IConnectors<decimal?, DecimalValidators> IsLessThan(decimal maximum);
    IConnectors<decimal?, DecimalValidators> MaximumIs(decimal maximum);
}