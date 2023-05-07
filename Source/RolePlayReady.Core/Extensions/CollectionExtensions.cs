namespace System.Extensions;

public static class CollectionExtensions {
    public static IConnectors<ICollection<TItem?>, CollectionValidators<TItem>> IsRequired<TItem>(this ICollection<TItem?>? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => Create(subject, source!);
    public static IConnectors<ICollection<TItem?>, CollectionValidators<TItem>> ForEach<TItem>(this ICollection<TItem?>? subject, Func<TItem?, ITerminator> validateItem, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => Create(subject, source!, validateItem);
}