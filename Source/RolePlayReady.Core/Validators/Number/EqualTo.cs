namespace System.Validators.Number;

public sealed class EqualTo<TValue> : NumberValidator<TValue>
    where TValue : IComparable<TValue> {

    public EqualTo(string source, TValue value)
        : base(source, value) {
    }

    protected override ValidationResult ValidateValue(NumberValidation<TValue> validation, TValue value)
        => validation.EqualTo(value).Result;
}