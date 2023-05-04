namespace System.Validators;

public abstract class CollectionValidator<TItem> : IValidator {
    private readonly string _source;

    protected CollectionValidator(string source) {
        _source = source;
    }

    public ValidationResult Validate(object? input, [CallerArgumentExpression(nameof(input))] string? source = null) {
        var value = (ICollection<TItem?>)input!;
        var validation = new CollectionValidations<TItem>(value, _source);
        return ValidateValue(validation).ToArray();
    }

    protected abstract ICollection<ValidationError> ValidateValue(CollectionValidations<TItem> validation);
}