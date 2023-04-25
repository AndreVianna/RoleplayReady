namespace System.Validators.Number;

public sealed class IsGreaterThan<TValue> : NumberValidator<TValue>
    where TValue : IComparable<TValue> {
    private readonly TValue _threshold;

    public IsGreaterThan(string source, TValue threshold)
        : base(source) {
        _threshold = threshold;
    }

    protected override Result ValidateValue(NumberValidation<TValue> validation)
        => validation.IsGreaterThan(_threshold).Result;
}