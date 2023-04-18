namespace System.Validators.Number;

public sealed class MinimumValueExclusive<TValue> : NumberValidator<TValue>
    where TValue : IComparable<TValue> {

    public MinimumValueExclusive(string source, TValue threshold)
        : base(source, threshold) {
    }

    protected override ValidationResult ValidateValue(NumberValidation<TValue> validation, TValue threshold)
        => validation.GreaterThan(threshold).Result;
}