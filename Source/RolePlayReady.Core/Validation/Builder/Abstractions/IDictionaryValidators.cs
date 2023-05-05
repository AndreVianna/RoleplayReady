namespace System.Validation.Builder.Abstractions;

public interface IDictionaryValidators<TKey, TValue> : IValidators<IDictionary<TKey, TValue?>, DictionaryValidators<TKey, TValue>>
    where TKey : notnull {
    IConnectors<IDictionary<TKey, TValue?>, DictionaryValidators<TKey, TValue>> IsNotEmpty();
    IConnectors<IDictionary<TKey, TValue?>, DictionaryValidators<TKey, TValue>> MaximumCountIs(int size);
    IConnectors<IDictionary<TKey, TValue?>, DictionaryValidators<TKey, TValue>> MinimumCountIs(int size);
    IConnectors<IDictionary<TKey, TValue?>, DictionaryValidators<TKey, TValue>> CountIs(int size);
    IConnectors<IDictionary<TKey, TValue?>, DictionaryValidators<TKey, TValue>> ContainsKey(TKey key);
    IConnectors<IDictionary<TKey, TValue?>, DictionaryValidators<TKey, TValue>> ForEach(Func<TValue?, IConnectors<TValue?, IValidators>> validateUsing);
}