namespace System.Validators.Number;

public sealed class MaximumValue<TValue> : NumberValidator<TValue>
    where TValue : IComparable<TValue> {

    public MaximumValue(string source, TValue threshold)
        : base(source, threshold) {
    }

    protected override ValidationResult ValidateValue(NumberValidation<TValue> validation, TValue threshold)
        => validation.LessOrEqualTo(threshold).Result;
}