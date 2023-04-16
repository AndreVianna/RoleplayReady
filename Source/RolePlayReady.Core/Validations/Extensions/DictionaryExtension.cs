namespace System.Validations.Extensions;

public static class DictionaryExtension
{
    public static IDictionaryValidations DictionaryIs<TKey, TValue>(this IDictionary<TKey, TValue> subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new DictionaryValidation<TKey, TValue>(subject, source);
    public static IDictionaryValueValidations<TKey, TValue> DictionaryValues<TKey, TValue>(this IDictionary<TKey, TValue> subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new DictionaryValueValidation<TKey, TValue>(subject, source);
}