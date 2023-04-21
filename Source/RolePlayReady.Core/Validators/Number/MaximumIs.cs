namespace System.Validators.Number;

public sealed class MaximumIs<TValue> : NumberValidator<TValue>
    where TValue : IComparable<TValue> {
    public MaximumIs(string source, TValue threshold)
        : base(source, threshold) {
    }

    protected override ValidationResult ValidateValue(NumberValidation<TValue> validation, TValue threshold)
        => validation.MaximumIs(threshold).Result;
}