namespace System.Validations.Abstractions;

public interface IDictionaryValidations<TKey, TValue> : IValidations<IDictionary<TKey, TValue?>, DictionaryValidations<TKey, TValue>>
    where TKey : notnull {
    IValidationsConnector<IDictionary<TKey, TValue?>, DictionaryValidations<TKey, TValue>> IsNotEmpty();
    IValidationsConnector<IDictionary<TKey, TValue?>, DictionaryValidations<TKey, TValue>> MaximumCountIs(int size);
    IValidationsConnector<IDictionary<TKey, TValue?>, DictionaryValidations<TKey, TValue>> MinimumCountIs(int size);
    IValidationsConnector<IDictionary<TKey, TValue?>, DictionaryValidations<TKey, TValue>> CountIs(int size);
    IValidationsConnector<IDictionary<TKey, TValue?>, DictionaryValidations<TKey, TValue>> ContainsKey(TKey key);
    IValidationsConnector<IDictionary<TKey, TValue?>, DictionaryValidations<TKey, TValue>> NotContainsKey(TKey key);
    IValidationsConnector<IDictionary<TKey, TValue?>, DictionaryValidations<TKey, TValue>> ForEach(Func<TValue?, IValidationsConnector<TValue?, IValidations>> validateUsing);
}