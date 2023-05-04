namespace System.Validations.Abstractions;

public interface IDictionaryValidations<TKey, TValue> : IValidations<IDictionary<TKey, TValue>>
    where TKey : notnull {
    IConnects<IDictionaryValidations<TKey, TValue?>> IsNotEmpty();
    IConnects<IDictionaryValidations<TKey, TValue?>> MaximumCountIs(int size);
    IConnects<IDictionaryValidations<TKey, TValue?>> MinimumCountIs(int size);
    IConnects<IDictionaryValidations<TKey, TValue?>> CountIs(int size);
    IConnects<IDictionaryValidations<TKey, TValue?>> ContainsKey(TKey key);
    IConnects<IDictionaryValidations<TKey, TValue?>> NotContainsKey(TKey key);
    ICollection<ValidationError> ForEach(Func<TValue?, ICollection<ValidationError>> validateUsing);
}