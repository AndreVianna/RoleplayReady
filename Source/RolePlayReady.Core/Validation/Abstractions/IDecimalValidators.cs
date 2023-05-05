namespace System.Validation.Abstractions;

public interface IDecimalValidators : IValidators<decimal?, DecimalValidators> {
    IValidatorsConnector<decimal?, DecimalValidators> MinimumIs(decimal minimum);
    IValidatorsConnector<decimal?, DecimalValidators> IsGreaterThan(decimal minimum);
    IValidatorsConnector<decimal?, DecimalValidators> IsEqualTo(decimal value);
    IValidatorsConnector<decimal?, DecimalValidators> IsLessThan(decimal maximum);
    IValidatorsConnector<decimal?, DecimalValidators> MaximumIs(decimal maximum);
}