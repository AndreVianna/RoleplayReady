namespace System.Validators.Number;

public sealed class IsGreaterThan<TValue> : NumberValidator<TValue>
    where TValue : IComparable<TValue> {

    public IsGreaterThan(string source, TValue threshold)
        : base(source, threshold) {
    }

    protected override ValidationResult ValidateValue(NumberValidation<TValue> validation, TValue threshold)
        => validation.IsGreaterThan(threshold).Result;
}