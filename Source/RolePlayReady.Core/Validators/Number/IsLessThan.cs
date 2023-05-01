namespace System.Validators.Number;

public sealed class IsLessThan<TValue> : NumberValidator<TValue>
    where TValue : IComparable<TValue> {
    private readonly TValue _threshold;

    public IsLessThan(string source, TValue threshold)
        : base(source) {
        _threshold = threshold;
    }

    protected override ValidationResult ValidateValue(NumberValidation<TValue> validation)
        => validation.IsLessThan(_threshold).Result;
}