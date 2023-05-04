namespace System.Validators.Number;

public sealed class MinimumIs<TValue> : NumberValidator<TValue>
    where TValue : struct, IComparable<TValue> {
    private readonly TValue _threshold;

    public MinimumIs(string source, TValue threshold)
        : base(source) {
        _threshold = threshold;
    }

    protected override ValidationResult ValidateValue(NumberValidations<TValue> validation)
        => validation.MinimumIs(_threshold).Result;
}