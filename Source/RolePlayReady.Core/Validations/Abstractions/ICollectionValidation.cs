namespace System.Validations.Abstractions;

public interface ICollectionValidation
    : IFinishesValidation,
      IConnectsToValidation<ICollectionValidation> {
    ICollectionValidation NotEmpty();
    ICollectionValidation NotShorterThan(int minimumCount);
    ICollectionValidation NotLongerThan(int maximumCount);
}