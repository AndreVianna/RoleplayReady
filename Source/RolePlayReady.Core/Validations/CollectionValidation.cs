namespace System.Validations;

public class CollectionValidation<TItem> :
    Validation<ICollection<TItem>?, ICollectionValidators<TItem>>,
    ICollectionValidation<TItem> {
    public CollectionValidation(ICollection<TItem>? subject, string? source, IEnumerable<ValidationError>? previousErrors = null)
        : base(subject, source, previousErrors) {
    }

    public IConnectsToOrFinishes<ICollectionValidators<TItem>> IsNotEmpty() {
        if (Subject is null) return this;
        if (!Subject.Any())
            Errors.Add(new(CannotBeEmpty, Source));
        return this;
    }

    public IConnectsToOrFinishes<ICollectionValidators<TItem>> MinimumCountIs(int size) {
        if (Subject is null) return this;
        if (Subject.Count < size)
            Errors.Add(new(CannotHaveLessThan, Source, size, Subject.Count));
        return this;
    }

    public IConnectsToOrFinishes<ICollectionValidators<TItem>> CountIs(int size) {
        if (Subject is null) return this;
        if (Subject.Count != size)
            Errors.Add(new(MustHave, Source, size, Subject.Count));
        return this;
    }

    public IConnectsToOrFinishes<ICollectionValidators<TItem>> Contains(TItem item) {
        if (Subject is null) return this;
        if (!Subject.Contains(item))
            Errors.Add(new(MustContain, Source, item));
        return this;
    }

    public IConnectsToOrFinishes<ICollectionValidators<TItem>> NotContains(TItem item) {
        if (Subject is null) return this;
        if (Subject.Contains(item))
            Errors.Add(new(MustNotContain, Source, item));
        return this;
    }

    public IConnectsToOrFinishes<ICollectionValidators<TItem>> MaximumCountIs(int size) {
        if (Subject is null) return this;
        if (Subject.Count > size)
            Errors.Add(new(CannotHaveMoreThan, Source, size, Subject.Count));
        return this;
    }

    public IFinishesValidation ForEach(Func<TItem, IFinishesValidation> validateUsing)
        => CollectionItemValidation.ForEachItemIn(this, validateUsing, false);
}