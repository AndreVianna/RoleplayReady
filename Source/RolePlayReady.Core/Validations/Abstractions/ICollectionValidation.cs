namespace System.Validations.Abstractions;

public interface ICollectionValidation<out TItem>
    : IConnectsToOrFinishes<ICollectionValidation<TItem>> {
    IConnectsToOrFinishes<ICollectionValidation<TItem>> NotEmpty();
    IConnectsToOrFinishes<ICollectionValidation<TItem>> NotShorterThan(int minimumCount);
    IConnectsToOrFinishes<ICollectionValidation<TItem>> NotLongerThan(int maximumCount);
    IFinishesValidation ForEach(Func<TItem, IFinishesValidation> validateUsing);
}