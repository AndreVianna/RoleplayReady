namespace System.Validators.Number;

public sealed class MaximumIs<TValue> : NumberValidator<TValue>
    where TValue : struct, IComparable<TValue> {
    private readonly TValue _threshold;

    public MaximumIs(string source, TValue threshold)
        : base(source) {
        _threshold = threshold;
    }

    protected override ValidationResult ValidateValue(NumberValidations<TValue> validation)
        => validation.MaximumIs(_threshold).Result;
}