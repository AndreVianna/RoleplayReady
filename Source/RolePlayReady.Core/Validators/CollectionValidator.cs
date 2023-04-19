namespace System.Validators;

public abstract class CollectionValidator<TItem> : IValidator {
    private readonly string _source;
    private readonly int _size;

    protected CollectionValidator(string source, int size) {
        _source = source;
        _size = size;
    }

    public ValidationResult Validate(object? input) {
        var value = (ICollection<TItem>)input!;
        var validation = new CollectionValidation<TItem>(value, _source);
        return ValidateValue(validation, _size);
    }

    protected abstract ValidationResult ValidateValue(CollectionValidation<TItem> validation, int threshold);
}