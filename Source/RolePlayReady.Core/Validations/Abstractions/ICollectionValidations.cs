namespace System.Validations.Abstractions;

public interface ICollectionValidations<TItem> : IValidations<ICollection<TItem>> {
    IConnects<ICollectionValidations<TItem?>> IsNotEmpty();
    IConnects<ICollectionValidations<TItem?>> MaximumCountIs(int size);
    IConnects<ICollectionValidations<TItem?>> MinimumCountIs(int size);
    IConnects<ICollectionValidations<TItem?>> CountIs(int size);
    IConnects<ICollectionValidations<TItem?>> Contains(TItem item);
    IConnects<ICollectionValidations<TItem?>> NotContains(TItem item);
    ICollection<ValidationError> ForEach(Func<TItem?, ICollection<ValidationError>> validateUsing);
}