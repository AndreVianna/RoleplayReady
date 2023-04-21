namespace System.Validations.Extensions;

public static class NumberExtensions {
    public static INumberValidators<TValue> Value<TValue>(this TValue subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        where TValue : IComparable<TValue>
        => new NumberValidation<TValue>(subject, source);

    public static INumberValidators<int> ValueIsNullOr(this int? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new NumberValidation<int>(subject, source);
    public static IConnectsToOrFinishes<INumberValidators<int>> IsNotNull(this int? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new NumberValidation<int>(subject, source, Validation.EnsureNotNull(subject, source));

    public static INumberValidators<decimal> ValueIsNullOr(this decimal? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new NumberValidation<decimal>(subject, source);
    public static IConnectsToOrFinishes<INumberValidators<decimal>> IsNotNull(this decimal? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new NumberValidation<decimal>(subject, source, Validation.EnsureNotNull(subject, source));
}