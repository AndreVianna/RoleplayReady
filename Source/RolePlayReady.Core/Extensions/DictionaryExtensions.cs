namespace System.Extensions;

public static class DictionaryExtensions {
    public static IConnectors<IDictionary<TKey, TValue?>, DictionaryValidators<TKey, TValue>> IsRequired<TKey, TValue>(this IDictionary<TKey, TValue?>? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        where TKey : notnull
        => Create(subject, source!);
    public static IConnectors<IDictionary<TKey, TValue?>, DictionaryValidators<TKey, TValue>> ForEach<TKey, TValue>(this IDictionary<TKey, TValue?>? subject, Func<TValue?, ITerminator> validate, [CallerArgumentExpression(nameof(subject))] string? source = null)
        where TKey : notnull
        => Create(subject, source!, validate);
}