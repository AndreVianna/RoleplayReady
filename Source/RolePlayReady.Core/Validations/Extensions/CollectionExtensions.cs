namespace System.Validations.Extensions;

public static class CollectionExtensions {
    public static IValidationsConnector<ICollection<TItem?>, CollectionValidations<TItem>> ForEach<TItem>(this ICollection<TItem?>? subject, Func<TItem?, IValidationsConnector<TItem?, IValidations>> validate, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => CollectionValidations<TItem>.CreateAsRequired(subject, source!).ForEach(validate);
}