namespace System.Validators;

public abstract class TextValidator : IValidator {
    private readonly string _source;

    protected TextValidator(string source) {
        _source = source;
    }

    public ValidationResult Validate(object? input, [CallerArgumentExpression(nameof(input))] string? source = null) {
        var value = (string)input!;
        var validation = new TextValidations(value.Trim(), _source);
        return ValidateValue(validation).ToArray();
    }

    protected abstract ICollection<ValidationError> ValidateValue(TextValidations validation);
}