namespace System.Validators;

public class NumericValidator<TNumber>
    : Validator<TNumber, INumericChecks, INumericConnectors>,
        INumericChecks,
        INumericConnectors {

    public NumericValidator(TNumber subject, string? source)
        : base(subject, source) {
    }
}