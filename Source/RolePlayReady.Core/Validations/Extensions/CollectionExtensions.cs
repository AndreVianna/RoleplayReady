namespace System.Validations.Extensions;

public static class CollectionExtensions {
    public static ICollectionValidations<TItem?> List<TItem>(this ICollection<TItem?>? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new CollectionValidations<TItem>(subject, source!, Validation.EnsureNotNull(subject, source!));
    public static List<ValidationError> ForEach<TItem>(this ICollection<TItem?>? subject, Func<TItem?, ICollection<ValidationError>> validateUsing, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new CollectionValidations<TItem>(subject, source!, Validation.EnsureNotNull(subject, source!))
           .ForEach(validateUsing).ToList();
}