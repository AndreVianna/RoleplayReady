namespace System.Validations;

public static class CollectionValidation {
    public static IFinishesValidation ForEachItemIn<TItem>(IValidation<ICollection<TItem>> validation, Func<TItem, IFinishesValidation> validateUsing) {
        if (validation.Subject is null) return validation;
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
    Validation<ICollection<TItem>, ICollectionValidation>,
    ICollectionValidation {

    public CollectionValidation(ICollection<TItem> subject, string? source, ICollection<ValidationError>? previousErrors = null)
        : base(subject, source, previousErrors) {
    }

    public ICollectionValidation NotEmpty() {
        if (Subject is null) return this;
        if (!Subject.Any()) Errors.Add(new(CannotBeEmpty, Source));
        return this;
    }

    public ICollectionValidation NotShorterThan(int minimumCount) {
        var count = Subject?.Count ?? 0;
        if (count < minimumCount)
            Errors.Add(new(CannotHaveLessThan, Source, minimumCount, count));
        return this;
    }

    public ICollectionValidation NotLongerThan(int maximumCount) {
        var count = Subject?.Count ?? 0;
        if (count > maximumCount)
            Errors.Add(new(CannotHaveMoreThan, Source, maximumCount, count));
        return this;
    }
}