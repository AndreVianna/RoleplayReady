namespace System.Validations;

public static class CollectionValidation {
    public static IFinishesValidation ForEachItemIn<TItem>(IValidation<IEnumerable<TItem>?> validation, Func<TItem, IFinishesValidation> validateUsing, bool addIsNullError = true) {
        if (validation.Subject is null) {
            if (addIsNullError) validation.Errors.Add(new(CannotBeNull, validation.Source));
            return validation;
        }

        var dictionary = validation.Subject as IDictionary;
        var index = 0;
        foreach (var item in validation.Subject) {
            var key = dictionary is not null ? item!.GetType().GetProperty("Key")!.GetValue(item) : index++;
            var source = $"{validation.Source}[{key}]";
            foreach (var error in validateUsing(item).Result.Errors) {
                var path = ((string)error.Arguments[0]!).Split('.');
                var skip = dictionary is not null && path.Length > 1 && path[1] == "Value" ? 2 : 1;
                error.Arguments[0] = path.Length > skip ? $"{source}.{string.Join('.', path[skip..])}" : source;
                validation.Errors.Add(error);
            }
        }

        return validation;
    }
}

public class CollectionValidation<TItem> :
    Validation<ICollection<TItem>?, ICollectionValidation<TItem>>,
    ICollectionValidation<TItem> {

    public CollectionValidation(ICollection<TItem>? subject, string? source, IEnumerable<ValidationError>? previousErrors = null)
        : base(subject, source, previousErrors) {
    }

    public IConnectsToOrFinishes<ICollectionValidation<TItem>> NotEmpty() {
        if (Subject is null) return this;
        if (!Subject.Any()) Errors.Add(new(CannotBeEmpty, Source));
        return this;
    }

    public IConnectsToOrFinishes<ICollectionValidation<TItem>> NotShorterThan(int minimumCount) {
        if (Subject is null) return this;
        if (Subject.Count < minimumCount)
            Errors.Add(new(CannotHaveLessThan, Source, minimumCount, Subject.Count));
        return this;
    }

    public IConnectsToOrFinishes<ICollectionValidation<TItem>> NotLongerThan(int maximumCount) {
        if (Subject is null) return this;
        if (Subject.Count > maximumCount)
            Errors.Add(new(CannotHaveMoreThan, Source, maximumCount, Subject.Count));
        return this;
    }

    public IFinishesValidation ForEach(Func<TItem, IFinishesValidation> validateUsing)
        => CollectionValidation.ForEachItemIn(this, validateUsing, false);
}