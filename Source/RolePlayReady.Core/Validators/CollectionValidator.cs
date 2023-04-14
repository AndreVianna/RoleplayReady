namespace System.Validators;

public class CollectionValidator<TItem> :
    Validator<ICollection<TItem>, ICollectionChecks<TItem>, ICollectionConnectors<TItem>>,
    ICollectionChecks<TItem>,
    ICollectionConnectors<TItem> {

    public CollectionValidator(ICollection<TItem> subject, string? source)
        : base(subject, source) {
    }

    public ICollectionConnectors<TItem> EachItem(Func<TItem, IValidatorResult> validateUsing) {
        if (Subject is null)
            return this;
        var index = 0;
        foreach (var item in Subject) {
            var source = $"{Source}[{index++}]";
            foreach (var error in validateUsing(item).Result.Errors) {
                error.Arguments[0] = source;
                Errors.Add(error);
            }
        }

        return this;
    }
}