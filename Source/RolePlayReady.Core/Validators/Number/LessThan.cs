namespace System.Validators.Number;

public sealed class LessThan<TValue> : NumberValidator<TValue>
    where TValue : IComparable<TValue> {

    public LessThan(string source, TValue threshold)
        : base(source, threshold) {
    }

    protected override ValidationResult ValidateValue(NumberValidation<TValue> validation, TValue threshold)
        => validation.LessThan(threshold).Result;
}