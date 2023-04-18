namespace System.Validators.Collection;

public sealed class ExactCount<TItem> : CollectionValidator<TItem> {

    public ExactCount(string source, int size)
        : base(source, size) {
    }

    protected override ValidationResult ValidateValue(CollectionValidation<TItem> validation, int size)
        => validation.Exactly(size).Result;
}