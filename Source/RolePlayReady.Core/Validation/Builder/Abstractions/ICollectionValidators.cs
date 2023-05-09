namespace System.Validation.Builder.Abstractions;

public interface ICollectionValidators<TItem> : IValidators {
    IConnectors<ICollection<TItem?>, CollectionValidators<TItem>> IsNotEmpty();
    IConnectors<ICollection<TItem?>, CollectionValidators<TItem>> AtMostHas(int size);
    IConnectors<ICollection<TItem?>, CollectionValidators<TItem>> HasAtLeast(int size);
    IConnectors<ICollection<TItem?>, CollectionValidators<TItem>> Has(int size);
    IConnectors<ICollection<TItem?>, CollectionValidators<TItem>> Contains(TItem item);

    IConnectors<ICollection<TItem?>, CollectionValidators<TItem>> Each(Func<TItem?, ITerminator> validate);
}
