namespace System.Validators;

public class NumericValidator<TNumber>
    : Validator<TNumber?, INumericChecks, INumericConnectors>,
        INumericChecks,
        INumericConnectors
    where TNumber : INumber<TNumber> {

    public NumericValidator(TNumber? subject, string? source)
        : base(subject, source) {
    }
}