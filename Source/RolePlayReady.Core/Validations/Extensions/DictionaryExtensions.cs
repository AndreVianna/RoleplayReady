namespace System.Validations.Extensions;

public static class DictionaryExtensions {
    public static IValidationsConnector<IDictionary<TKey, TValue?>, DictionaryValidations<TKey, TValue>> IsRequired<TKey, TValue>(this IDictionary<TKey, TValue?>? subject, Func<TValue?, IValidationsConnector<TValue?, IValidations>> validate, [CallerArgumentExpression(nameof(subject))] string? source = null)
        where TKey : notnull
        => DictionaryValidations<TKey, TValue>.CreateAsRequired(subject, source!).ForEach(validate);
}