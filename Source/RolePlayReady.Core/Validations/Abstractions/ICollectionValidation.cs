namespace System.Validations.Abstractions;

public interface ICollectionValidation<out TItem>
    : IConnectsToOrFinishes<ICollectionValidators<TItem>>,
      ICollectionValidators<TItem> {
}

public interface ICollectionValidators<out TItem> {
    IConnectsToOrFinishes<ICollectionValidators<TItem>> IsNotEmpty();
    IConnectsToOrFinishes<ICollectionValidators<TItem>> MaximumCountIs(int size);
    IConnectsToOrFinishes<ICollectionValidators<TItem>> MinimumCountIs(int size);
    IConnectsToOrFinishes<ICollectionValidators<TItem>> CountIs(int size);
    IFinishesValidation ForEach(Func<TItem, IFinishesValidation> validateUsing);
}