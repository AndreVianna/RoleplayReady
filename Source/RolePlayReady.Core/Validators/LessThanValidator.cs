namespace System.Validators;

public sealed class LessThanValidator<TValue> : NumberValidator<TValue>
    where TValue : IComparable<TValue> {

    public LessThanValidator(string source, TValue maximum)
        : base(source, maximum) {
    }

    protected override ValidationResult ValidateNumber(NumberValidation<TValue> validation, TValue threshold)
        => validation.LessThan(threshold).Result;
}