namespace System.Validations.Extensions;

public static class CollectionExtensions {
    public static ICollectionValidators<TItem> List<TItem>(this ICollection<TItem>? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new CollectionValidation<TItem>(subject, source, Validation.EnsureNotNull(subject, source));
    public static ICollectionValidators<KeyValuePair<TKey, TValue>> List<TKey, TValue>(this ICollection<KeyValuePair<TKey, TValue>>? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new CollectionValidation<KeyValuePair<TKey, TValue>>(subject, source, Validation.EnsureNotNull(subject, source));
    public static IFinishesValidation ForEach<TItem>(this ICollection<TItem>? subject, Func<TItem, IFinishesValidation> validateUsing, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => CollectionValidation.ForEachItemIn(new CollectionValidation<TItem>(subject, source), validateUsing);
}
