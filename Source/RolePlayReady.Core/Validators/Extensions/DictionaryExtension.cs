namespace System.Validators.Extensions;

public static class DictionaryExtension
{
    public static IDictionaryChecks<TKey, TValue> DictionaryIs<TKey, TValue>(this IDictionary<TKey, TValue> subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new DictionaryValidator<TKey, TValue>(subject, source);
}