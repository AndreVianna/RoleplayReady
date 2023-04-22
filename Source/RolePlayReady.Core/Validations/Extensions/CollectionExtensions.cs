namespace System.Validations.Extensions;

public static class CollectionExtensions {
    public static ICollectionValidators<TItem> List<TItem>(this ICollection<TItem>? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new CollectionValidation<TItem>(subject, source, Validation.EnsureNotNull(subject, source));
    public static IFinishesValidation ForEach<TItem>(this ICollection<TItem>? subject, Func<TItem, IFinishesValidation> validateUsing, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => CollectionItemValidation.ForEachItemIn(new CollectionValidation<TItem>(subject, source), validateUsing);
}