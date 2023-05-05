using System.Validation;
using System.Validation.Abstractions;

namespace System.Extensions;

public static class DictionaryExtensions {
    public static IValidatorsConnector<IDictionary<TKey, TValue?>, DictionaryValidators<TKey, TValue>> IsRequired<TKey, TValue>(this IDictionary<TKey, TValue?>? subject, Func<TValue?, IValidatorsConnector<TValue?, IValidators>> validate, [CallerArgumentExpression(nameof(subject))] string? source = null)
        where TKey : notnull
        => DictionaryValidators<TKey, TValue>.CreateAsRequired(subject, source!).ForEach(validate);
}