namespace System.Validators;

public abstract class NumberValidator<TNumber> : IValidator
    where TNumber : IComparable<TNumber> {
    private readonly string _source;

    protected NumberValidator(string source) {
        _source = source;
    }

    public Result Validate(object? input) {
        var value = (TNumber)input!;
        var validation = new NumberValidation<TNumber>(value, _source);
        return ValidateValue(validation);
    }

    protected abstract Result ValidateValue(NumberValidation<TNumber> validation);
}