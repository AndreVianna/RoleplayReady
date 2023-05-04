namespace System.Validations.Extensions;

public static class ComparableExtensions {
    public static INumberValidations<TValue> Value<TValue>(this TValue subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        where TValue : IComparable<TValue>
        => new NumberValidations<TValue>(subject, source!);
}