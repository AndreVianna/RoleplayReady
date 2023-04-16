namespace System.Validations.Extensions;

public static class CollectionExtension {
    public static ICollectionValidations CollectionIs<TItem>(this ICollection<TItem> subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new CollectionValidation<TItem>(subject, source);
    public static ICollectionItemValidations<TItem> CollectionItems<TItem>(this ICollection<TItem> subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new CollectionItemValidation<TItem>(subject, source);
}
