namespace System.Validators;

public abstract class NumberValidator<TNumber> : IValidator
    where TNumber : struct, IComparable<TNumber> {
    private readonly string _source;

    protected NumberValidator(string source) {
        _source = source;
    }

    public ValidationResult Validate(object? input, [CallerArgumentExpression(nameof(input))] string? source = null) {
        var value = (TNumber)input!;
        var validation = NumberValidations<TNumber>.CreateAsOptional<TNumber>(value, _source);
        return ValidateValue(validation);
    }

    protected abstract ValidationResult ValidateValue(NumberValidations<TNumber> validation);
}