namespace System.Validators.Number;

public sealed class MinimumIs<TValue> : NumberValidator<TValue>
    where TValue : IComparable<TValue> {
    private readonly TValue _threshold;

    public MinimumIs(string source, TValue threshold)
        : base(source) {
        _threshold = threshold;
    }

    protected override ICollection<ValidationError> ValidateValue(NumberValidations<TValue> validation)
        => validation.MinimumIs(_threshold).Errors;
}