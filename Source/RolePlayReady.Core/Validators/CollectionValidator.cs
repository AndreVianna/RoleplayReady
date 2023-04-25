namespace System.Validators;

public abstract class CollectionValidator<TItem> : IValidator {
    private readonly string _source;

    protected CollectionValidator(string source) {
        _source = source;
    }

    public Result Validate(object? input) {
        var value = (ICollection<TItem>)input!;
        var validation = new CollectionValidation<TItem>(value, _source);
        return ValidateValue(validation);
    }

    protected abstract Result ValidateValue(CollectionValidation<TItem> validation);
}