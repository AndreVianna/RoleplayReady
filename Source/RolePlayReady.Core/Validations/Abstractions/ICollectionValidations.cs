namespace System.Validations.Abstractions;

public interface ICollectionValidations<TItem> : IValidations<ICollection<TItem?>, CollectionValidations<TItem>> {
    IValidationsConnector<ICollection<TItem?>, CollectionValidations<TItem>> IsNotEmpty();
    IValidationsConnector<ICollection<TItem?>, CollectionValidations<TItem>> MaximumCountIs(int size);
    IValidationsConnector<ICollection<TItem?>, CollectionValidations<TItem>> MinimumCountIs(int size);
    IValidationsConnector<ICollection<TItem?>, CollectionValidations<TItem>> CountIs(int size);
    IValidationsConnector<ICollection<TItem?>, CollectionValidations<TItem>> Contains(TItem item);
    IValidationsConnector<ICollection<TItem?>, CollectionValidations<TItem>> NotContains(TItem item);
    IValidationsConnector<ICollection<TItem?>, CollectionValidations<TItem>> ForEach(Func<TItem?, IValidationsConnector<TItem?, IValidations>> validate);
}