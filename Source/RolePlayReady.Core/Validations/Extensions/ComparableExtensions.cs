namespace System.Validations.Extensions;

public static class ComparableExtensions {
    public static INumberValidators<TValue> Value<TValue>(this TValue subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        where TValue : IComparable<TValue>
        => new NumberValidation<TValue>(subject, source);
}