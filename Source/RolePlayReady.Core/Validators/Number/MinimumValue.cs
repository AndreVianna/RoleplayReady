namespace System.Validators.Number;

public sealed class MinimumValue<TValue> : NumberValidator<TValue>
    where TValue : IComparable<TValue> {

    public MinimumValue(string source, TValue threshold)
        : base(source, threshold) {
    }

    protected override ValidationResult ValidateValue(NumberValidation<TValue> validation, TValue threshold)
        => validation.GreaterOrEqualTo(threshold).Result;
}