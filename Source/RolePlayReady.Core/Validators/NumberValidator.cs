namespace System.Validators;

public abstract class NumberValidator<TNumber> : IValidator
    where TNumber : IComparable<TNumber> {
    private readonly string _source;

    protected NumberValidator(string source) {
        _source = source;
    }

    public ValidationResult Validate(object? input, [CallerArgumentExpression(nameof(input))] string? source = null) {
        var value = (TNumber)input!;
        var validation = new NumberValidations<TNumber>(value, _source);
        return ValidateValue(validation).ToArray();
    }

    protected abstract ICollection<ValidationError> ValidateValue(NumberValidations<TNumber> validation);
}