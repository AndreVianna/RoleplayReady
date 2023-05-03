namespace System.Validations.Abstractions;

public interface IDictionaryValidation<in TKey, out TValue>
    : IConnectsToOrFinishes<IDictionaryValidators<TKey, TValue?>>,
      IDictionaryValidators<TKey, TValue?> {
}