namespace System.Validations.Extensions;

public static class DictionaryExtensions {
    public static IDictionaryValidations<TKey, TValue?> Map<TKey, TValue>(this IDictionary<TKey, TValue?>? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        where TKey : notnull
        => new DictionaryValidations<TKey, TValue>(subject, source!, Validation.EnsureNotNull(subject, source!));
    public static ICollection<ValidationError> ForEach<TKey, TValue>(this IDictionary<TKey, TValue?>? subject, Func<TValue?, ICollection<ValidationError>> validateUsing, [CallerArgumentExpression(nameof(subject))] string? source = null)
        where TKey : notnull
        => new DictionaryValidations<TKey, TValue>(subject, source!, Validation.EnsureNotNull(subject, source!))
           .ForEach(validateUsing);
}