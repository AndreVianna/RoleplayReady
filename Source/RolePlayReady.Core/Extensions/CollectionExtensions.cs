namespace System.Extensions;

public static class CollectionExtensions {
    public static IConnectors<ICollection<TItem?>, CollectionValidators<TItem>> IsRequired<TItem>(this ICollection<TItem?>? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => CollectionValidators<TItem>.CreatedAndConnect(subject, source!);
    public static IConnectors<ICollection<TItem?>, CollectionValidators<TItem>> ForEach<TItem>(this ICollection<TItem?>? subject, Func<TItem?, ITerminator> validate, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => CollectionValidators<TItem>.Create(subject, source!).ForEach(validate);
}