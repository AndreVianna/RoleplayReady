namespace System.Validations.Extensions;

public static class DictionaryExtensions {
    public static IDictionaryValidators<TKey, TValue?> Map<TKey, TValue>(this IDictionary<TKey, TValue?>? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new DictionaryValidation<TKey, TValue>(subject, source, Validation.EnsureNotNull(subject, source));
    public static IFinishesValidation ForEach<TKey, TValue>(this IDictionary<TKey, TValue?>? subject, Func<TValue?, IFinishesValidation> validateUsing, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => DictionaryItemValidation.ForEachItemIn(new DictionaryValidation<TKey, TValue>(subject, source), validateUsing);
}