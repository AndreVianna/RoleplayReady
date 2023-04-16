namespace System.Validations.Extensions;

public static class CollectionExtension {
    public static ICollectionValidation ListIs<TItem>(this ICollection<TItem> subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new CollectionValidation<TItem>(subject, source);
    public static IFinishesValidation ForEach<TItem>(this ICollection<TItem> subject, Func<TItem, IFinishesValidation> validateUsing, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => CollectionValidation.ForEachItemIn(new CollectionValidation<TItem>(subject, source), validateUsing);
    public static ICollectionValidation MappingIs<TKey, TValue>(this IDictionary<TKey, TValue> subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new CollectionValidation<KeyValuePair<TKey, TValue>>(subject, source);
}
