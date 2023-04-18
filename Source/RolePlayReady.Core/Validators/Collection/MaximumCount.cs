namespace System.Validators.Collection;

public sealed class MaximumCount<TItem> : CollectionValidator<TItem> {

    public MaximumCount(string source, int size)
        : base(source, size) {
    }

    protected override ValidationResult ValidateValue(CollectionValidation<TItem> validation, int size)
        => validation.NotBiggerThan(size).Result;
}