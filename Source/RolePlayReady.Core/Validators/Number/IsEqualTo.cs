namespace System.Validators.Number;

public sealed class IsEqualTo<TValue> : NumberValidator<TValue>
    where TValue : struct, IComparable<TValue> {
    private readonly TValue _value;

    public IsEqualTo(string source, TValue value)
        : base(source) {
        _value = value;
    }

    protected override ValidationResult ValidateValue(NumberValidations<TValue> validation)
        => validation.IsEqualTo(_value).Result;
}