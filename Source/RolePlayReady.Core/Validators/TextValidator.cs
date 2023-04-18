namespace System.Validators;

public abstract class TextValidator : IValidator {
    private readonly string _source;
    private readonly int _length;

    protected TextValidator(string source, int length) {
        _source = source;
        _length = length;
    }

    public ValidationResult Validate(object? input) {
        if (input is string value) {
            var validation = new StringValidation(value.Trim(), _source);
            return ValidateValue(validation, _length);
        }

        return input is null
            ? new ValidationError(CannotBeNull, _source)
            : new ValidationError(IsNotOfType, _source, nameof(DateTime), input.GetType().Name);
    }

    protected abstract ValidationResult ValidateValue(StringValidation validation, int length);
}