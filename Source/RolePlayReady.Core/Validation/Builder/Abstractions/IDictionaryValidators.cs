namespace System.Validation.Builder.Abstractions;

public interface IDictionaryValidators<TKey, TValue> : IValidators
    where TKey : notnull {
    IConnectors<IDictionary<TKey, TValue?>, DictionaryValidators<TKey, TValue>> IsNotEmpty();
    IConnectors<IDictionary<TKey, TValue?>, DictionaryValidators<TKey, TValue>> HasAtMost(int size);
    IConnectors<IDictionary<TKey, TValue?>, DictionaryValidators<TKey, TValue>> HasAtLeast(int size);
    IConnectors<IDictionary<TKey, TValue?>, DictionaryValidators<TKey, TValue>> Has(int size);
    IConnectors<IDictionary<TKey, TValue?>, DictionaryValidators<TKey, TValue>> ContainsKey(TKey key);

    IConnectors<IDictionary<TKey, TValue?>, DictionaryValidators<TKey, TValue>> Each(Func<TValue?, ITerminator> validateUsing);
}