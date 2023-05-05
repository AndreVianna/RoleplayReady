using System.Validation;
using System.Validation.Abstractions;

namespace System.Extensions;

public static class CollectionExtensions {
    public static IValidatorsConnector<ICollection<TItem?>, CollectionValidators<TItem>> ForEach<TItem>(this ICollection<TItem?>? subject, Func<TItem?, IValidatorsConnector<TItem?, IValidators>> validate, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => CollectionValidators<TItem>.CreateAsRequired(subject, source!).ForEach(validate);
}