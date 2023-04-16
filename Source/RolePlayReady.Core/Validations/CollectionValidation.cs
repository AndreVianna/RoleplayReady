namespace System.Validations;
public class CollectionValidation<TItem> :
    Validation<ICollection<TItem>, CollectionValidation<TItem>, ICollectionValidations>,
    ICollectionValidations {

    public CollectionValidation(ICollection<TItem> subject, string? source, ICollection<ValidationError>? previousErrors = null)
        : base(subject, source, previousErrors) {
    }

    public IConnectors<ICollectionValidations> NotEmpty() {
        if (Subject is null) return this;
        if (!Subject.Any()) Errors.Add(new(CannotBeEmpty, Source));
        return this;
    }
}