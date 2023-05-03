namespace System.Validations.Abstractions;

public interface ICollectionValidation<TItem>
    : IConnectsToOrFinishes<ICollectionValidators<TItem?>>,
      ICollectionValidators<TItem?> {
}