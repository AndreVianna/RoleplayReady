namespace System.Validators.Number;

public sealed class IsEqualTo<TValue> : NumberValidator<TValue>
    where TValue : IComparable<TValue> {
    public IsEqualTo(string source, TValue value)
        : base(source, value) {
    }

    protected override ValidationResult ValidateValue(NumberValidation<TValue> validation, TValue value)
        => validation.IsEqualTo(value).Result;
}