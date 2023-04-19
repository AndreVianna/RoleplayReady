namespace System.Validators.Number;

public sealed class MinimumIs<TValue> : NumberValidator<TValue>
    where TValue : IComparable<TValue> {

    public MinimumIs(string source, TValue threshold)
        : base(source, threshold) {
    }

    protected override ValidationResult ValidateValue(NumberValidation<TValue> validation, TValue threshold)
        => validation.MinimumIs(threshold).Result;
}