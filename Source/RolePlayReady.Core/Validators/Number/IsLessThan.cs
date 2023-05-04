namespace System.Validators.Number;

public sealed class IsLessThan<TValue> : NumberValidator<TValue>
    where TValue : struct, IComparable<TValue> {
    private readonly TValue _threshold;

    public IsLessThan(string source, TValue threshold)
        : base(source) {
        _threshold = threshold;
    }

    protected override ValidationResult ValidateValue(NumberValidations<TValue> validation)
        => validation.IsLessThan(_threshold).Result;
}