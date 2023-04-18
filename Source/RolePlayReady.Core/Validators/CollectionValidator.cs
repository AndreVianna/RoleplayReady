namespace System.Validators;

public abstract class CollectionValidator<TItem> : IValidator {
    private readonly string _source;
    private readonly int _size;

    protected CollectionValidator(string source, int size) {
        _source = source;
        _size = size;
    }

    public ValidationResult Validate(object? input) {
        if (input is ICollection<TItem> value) {
            var validation = new CollectionValidation<TItem>(value, _source);
            return ValidateValue(validation, _size);
        }

        return input is null
            ? new ValidationError(CannotBeNull, _source)
            : new ValidationError(IsNotOfType, _source, typeof(TItem).Name, input.GetType().Name);
    }

    protected abstract ValidationResult ValidateValue(CollectionValidation<TItem> validation, int threshold);
}