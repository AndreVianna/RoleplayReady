namespace System.Validators.Number;

public sealed class MaximumValueIs<TValue> : NumberValidator<TValue>
    where TValue : IComparable<TValue> {

    public MaximumValueIs(string source, TValue threshold)
        : base(source, threshold) {
    }

    protected override ValidationResult ValidateValue(NumberValidation<TValue> validation, TValue threshold)
        => validation.LessOrEqualTo(threshold).Result;
}