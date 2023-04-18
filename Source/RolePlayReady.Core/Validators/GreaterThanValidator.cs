namespace System.Validators;

public sealed class GreaterThanValidator<TValue> : NumberValidator<TValue>
    where TValue : IComparable<TValue> {

    public GreaterThanValidator(string source, TValue maximum)
        : base(source, maximum) {
    }

    protected override ValidationResult ValidateNumber(NumberValidation<TValue> validation, TValue threshold)
        => validation.GreaterThan(threshold).Result;
}