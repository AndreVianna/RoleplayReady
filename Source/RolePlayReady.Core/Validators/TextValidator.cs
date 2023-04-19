namespace System.Validators;

public abstract class TextValidator : IValidator {
    private readonly string _source;
    private readonly int _length;

    protected TextValidator(string source, int length) {
        _source = source;
        _length = length;
    }

    public ValidationResult Validate(object? input) {
        var value = (string)input!;
        var validation = new StringValidation(value.Trim(), _source);
        return ValidateValue(validation, _length);
    }

    protected abstract ValidationResult ValidateValue(StringValidation validation, int length);
}