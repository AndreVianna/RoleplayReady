namespace System.Validations.Abstractions;

public interface ICollectionValidation<out TItem>
    : IConnectsToOrFinishes<ICollectionValidation<TItem>> {
    IConnectsToOrFinishes<ICollectionValidation<TItem>> NotEmpty();
    IConnectsToOrFinishes<ICollectionValidation<TItem>> NotSmallerThan(int size);
    IConnectsToOrFinishes<ICollectionValidation<TItem>> NotBiggerThan(int size);
    IFinishesValidation ForEach(Func<TItem, IFinishesValidation> validateUsing);
}