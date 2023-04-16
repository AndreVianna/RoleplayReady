namespace System.Validations;

public class CollectionItemValidation<TItem> :
    Validation<ICollection<TItem>, CollectionItemValidation<TItem>, ICollectionItemValidations<TItem>>,
    ICollectionItemValidations<TItem> {

    public CollectionItemValidation(ICollection<TItem> subject, string? source, ICollection<ValidationError>? previousErrors = null)
        : base(subject, source, previousErrors) {
    }

    public IConnectors<ICollectionItemValidations<TItem>> Each(Func<TItem, IFinishValidation> validateUsing) {
        if (Subject is null) return this;
        var index = 0;
        foreach (var item in Subject) {
            var source = $"{Source}[{index++}]";
            foreach (var error in validateUsing(item).Result.Errors) {
                var path = ((string)error.Arguments[0]!).Split('.');
                error.Arguments[0] = path.Length > 1 ? $"{source}.{string.Join('.', path[1..])}" : source;
                Errors.Add(error);
            }
        }

        return this;
    }
}