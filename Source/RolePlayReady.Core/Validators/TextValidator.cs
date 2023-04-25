namespace System.Validators;

public abstract class TextValidator : IValidator {
    private readonly string _source;

    protected TextValidator(string source) {
        _source = source;
    }

    public Result Validate(object? input) {
        var value = (string)input!;
        var validation = new TextValidation(value.Trim(), _source);
        return ValidateValue(validation);
    }

    protected abstract Result ValidateValue(TextValidation validation);
}