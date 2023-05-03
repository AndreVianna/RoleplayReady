namespace System.Validators.Abstractions;

public interface IDictionaryValidators<in TKey, out TValue> {
    IConnectsToOrFinishes<IDictionaryValidators<TKey, TValue?>> IsNotEmpty();
    IConnectsToOrFinishes<IDictionaryValidators<TKey, TValue?>> MaximumCountIs(int size);
    IConnectsToOrFinishes<IDictionaryValidators<TKey, TValue?>> MinimumCountIs(int size);
    IConnectsToOrFinishes<IDictionaryValidators<TKey, TValue?>> CountIs(int size);
    IConnectsToOrFinishes<IDictionaryValidators<TKey, TValue?>> ContainsKey(TKey key);
    IConnectsToOrFinishes<IDictionaryValidators<TKey, TValue?>> NotContainsKey(TKey key);
    IFinishesValidation ForEach(Func<TValue?, IFinishesValidation> validateUsing);
}