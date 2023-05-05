namespace System.Validation.Abstractions;

public interface ICollectionValidators<TItem> : IValidators<ICollection<TItem?>, CollectionValidators<TItem>> {
    IValidatorsConnector<ICollection<TItem?>, CollectionValidators<TItem>> IsNotEmpty();
    IValidatorsConnector<ICollection<TItem?>, CollectionValidators<TItem>> MaximumCountIs(int size);
    IValidatorsConnector<ICollection<TItem?>, CollectionValidators<TItem>> MinimumCountIs(int size);
    IValidatorsConnector<ICollection<TItem?>, CollectionValidators<TItem>> CountIs(int size);
    IValidatorsConnector<ICollection<TItem?>, CollectionValidators<TItem>> Contains(TItem item);
    IValidatorsConnector<ICollection<TItem?>, CollectionValidators<TItem>> ForEach(Func<TItem?, IValidatorsConnector<TItem?, IValidators>> validate);
}