namespace System.Validators.Abstractions;

public interface ICollectionValidators<TItem> {
    IConnectsToOrFinishes<ICollectionValidators<TItem?>> IsNotEmpty();
    IConnectsToOrFinishes<ICollectionValidators<TItem?>> MaximumCountIs(int size);
    IConnectsToOrFinishes<ICollectionValidators<TItem?>> MinimumCountIs(int size);
    IConnectsToOrFinishes<ICollectionValidators<TItem?>> CountIs(int size);
    IConnectsToOrFinishes<ICollectionValidators<TItem?>> Contains(TItem item);
    IConnectsToOrFinishes<ICollectionValidators<TItem?>> NotContains(TItem item);
    IFinishesValidation ForEach(Func<TItem?, IFinishesValidation> validateUsing);
}