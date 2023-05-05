﻿namespace System.Validation.Builder.Abstractions;

public interface ICollectionValidators<TItem> : IValidators<ICollection<TItem?>, CollectionValidators<TItem>> {
    IConnectors<ICollection<TItem?>, CollectionValidators<TItem>> IsNotEmpty();
    IConnectors<ICollection<TItem?>, CollectionValidators<TItem>> MaximumCountIs(int size);
    IConnectors<ICollection<TItem?>, CollectionValidators<TItem>> MinimumCountIs(int size);
    IConnectors<ICollection<TItem?>, CollectionValidators<TItem>> CountIs(int size);
    IConnectors<ICollection<TItem?>, CollectionValidators<TItem>> Contains(TItem item);
    IConnectors<ICollection<TItem?>, CollectionValidators<TItem>> ForEach(Func<TItem?, IConnectors<TItem?, IValidators>> validate);
}