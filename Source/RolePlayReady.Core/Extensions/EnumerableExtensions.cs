namespace System.Extensions;

public static class EnumerableExtensions {
    public static TItem[] ToArray<TItem>(this IEnumerable<TItem>? source, Func<TItem, TItem> transform) => source?.ToArray<TItem, TItem>(transform) ?? [];

    public static TOutput[] ToArray<TItem, TOutput>(this IEnumerable<TItem>? source, Func<TItem, TOutput> transform) => source?.Select(transform).ToArray() ?? [];
}
