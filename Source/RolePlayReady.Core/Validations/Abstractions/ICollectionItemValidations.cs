namespace System.Validations.Abstractions;

public interface ICollectionItemValidations<out TItem> : IValidations {
    IConnectors<ICollectionItemValidations<TItem>> Each(Func<TItem, IFinishValidation> validateUsing);
}