namespace System.Validators.Collection;

public sealed class MinimumCountIs<TItem> : CollectionValidator<TItem> {

    public MinimumCountIs(string source, int size)
        : base(source, size) {
    }

    protected override ValidationResult ValidateValue(CollectionValidation<TItem> validation, int size)
        => validation.NotSmallerThan(size).Result;
}