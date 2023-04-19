namespace System.Validators.Collection;

public sealed class CountIs<TItem> : CollectionValidator<TItem> {

    public CountIs(string source, int size)
        : base(source, size) {
    }

    protected override ValidationResult ValidateValue(CollectionValidation<TItem> validation, int size)
        => validation.Exactly(size).Result;
}