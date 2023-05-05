namespace System.Validation.Abstractions;

public interface IDictionaryValidators<TKey, TValue> : IValidators<IDictionary<TKey, TValue?>, DictionaryValidators<TKey, TValue>>
    where TKey : notnull {
    IValidatorsConnector<IDictionary<TKey, TValue?>, DictionaryValidators<TKey, TValue>> IsNotEmpty();
    IValidatorsConnector<IDictionary<TKey, TValue?>, DictionaryValidators<TKey, TValue>> MaximumCountIs(int size);
    IValidatorsConnector<IDictionary<TKey, TValue?>, DictionaryValidators<TKey, TValue>> MinimumCountIs(int size);
    IValidatorsConnector<IDictionary<TKey, TValue?>, DictionaryValidators<TKey, TValue>> CountIs(int size);
    IValidatorsConnector<IDictionary<TKey, TValue?>, DictionaryValidators<TKey, TValue>> ContainsKey(TKey key);
    IValidatorsConnector<IDictionary<TKey, TValue?>, DictionaryValidators<TKey, TValue>> ForEach(Func<TValue?, IValidatorsConnector<TValue?, IValidators>> validateUsing);
}