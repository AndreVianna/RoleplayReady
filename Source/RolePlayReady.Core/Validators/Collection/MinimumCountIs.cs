namespace System.Validators.Collection;

public sealed class MinimumCountIs<TItem> : CollectionValidator<TItem> {
    private readonly int _count;

    public MinimumCountIs(string source, int count)
        : base(source) {
        _count = count;
    }

    protected override ValidationResult ValidateValue(CollectionValidation<TItem> validation)
        => validation.MinimumCountIs(_count).Result;
}