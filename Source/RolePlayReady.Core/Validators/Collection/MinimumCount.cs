namespace System.Validators.Collection;

public sealed class MinimumCount<TItem> : CollectionValidator<TItem> {

    public MinimumCount(string source, int size)
        : base(source, size) {
    }

    protected override ValidationResult ValidateValue(CollectionValidation<TItem> validation, int size)
        => validation.NotSmallerThan(size).Result;
}