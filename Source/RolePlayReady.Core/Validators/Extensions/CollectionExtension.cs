namespace System.Validators.Extensions;

public static class CollectionExtension {
    public static ICollectionChecks<TItem> CollectionIs<TItem>(this ICollection<TItem> subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new CollectionValidator<TItem>(subject, source);
}
