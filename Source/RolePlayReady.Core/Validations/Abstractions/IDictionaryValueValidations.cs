namespace System.Validations.Abstractions;

public interface IDictionaryValueValidations<TKey, out TValue> : IValidations {
    IConnectors<IDictionaryValueValidations<TKey, TValue>> Each(Func<TValue, IFinishValidation> validateUsing);
}