namespace System.Validators.Number;

public sealed class MinimumValueIs<TValue> : NumberValidator<TValue>
    where TValue : IComparable<TValue> {

    public MinimumValueIs(string source, TValue threshold)
        : base(source, threshold) {
    }

    protected override ValidationResult ValidateValue(NumberValidation<TValue> validation, TValue threshold)
        => validation.GreaterOrEqualTo(threshold).Result;
}