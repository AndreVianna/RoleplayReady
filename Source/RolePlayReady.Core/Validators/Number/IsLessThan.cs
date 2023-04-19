namespace System.Validators.Number;

public sealed class IsLessThan<TValue> : NumberValidator<TValue>
    where TValue : IComparable<TValue> {

    public IsLessThan(string source, TValue threshold)
        : base(source, threshold) {
    }

    protected override ValidationResult ValidateValue(NumberValidation<TValue> validation, TValue threshold)
        => validation.IsLessThan(threshold).Result;
}