namespace System.Validators.Collection;

public sealed class MaximumCountIs<TItem> : CollectionValidator<TItem> {

    public MaximumCountIs(string source, int size)
        : base(source, size) {
    }

    protected override ValidationResult ValidateValue(CollectionValidation<TItem> validation, int size)
        => validation.MaximumCountIs(size).Result;
}