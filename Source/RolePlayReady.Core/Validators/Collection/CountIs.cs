namespace System.Validators.Collection;

public sealed class CountIs<TItem> : CollectionValidator<TItem> {
    private readonly int _count;

    public CountIs(string source, int count)
        : base(source) {
        _count = count;
    }

    protected override ICollection<ValidationError> ValidateValue(CollectionValidations<TItem> validation)
        => validation.CountIs(_count).Errors;
}