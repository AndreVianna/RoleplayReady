namespace System.Validators.Number;

public sealed class MaximumIs<TValue> : NumberValidator<TValue>
    where TValue : IComparable<TValue> {
    private readonly TValue _threshold;

    public MaximumIs(string source, TValue threshold)
        : base(source) {
        _threshold = threshold;
    }

    protected override Result ValidateValue(NumberValidation<TValue> validation)
        => validation.MaximumIs(_threshold).Result;
}