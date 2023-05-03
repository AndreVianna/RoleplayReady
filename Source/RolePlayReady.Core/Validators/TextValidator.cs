namespace System.Validators;

public abstract class TextValidator : IValidator {
    private readonly string _source;

    protected TextValidator(string source) {
        _source = source;
    }

    public ValidationResult Validate(object? input, [CallerArgumentExpression(nameof(input))] string? source = null) {
        var value = (string)input!;
        var validation = new TextValidation(value.Trim(), _source);
        return ValidateValue(validation);
    }

    protected abstract ValidationResult ValidateValue(TextValidation validation);
}