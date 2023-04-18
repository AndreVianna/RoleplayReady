namespace System.Validations.Extensions;

public static class NumberExtensions {
    public static INumberValidation<TValue> Is<TValue>(this TValue subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        where TValue : IComparable<TValue>
        => new NumberValidation<TValue>(subject, source);
    public static INumberValidation<int> IsNullOr(this int? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new NumberValidation<int>(subject, source);
    public static IConnectsToOrFinishes<INumberValidation<int>> IsNotNull(this int? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new NumberValidation<int>(subject, source, Validation.EnsureNotNull(subject, source));
    public static INumberValidation<decimal> IsNullOr(this decimal? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new NumberValidation<decimal>(subject, source);
    public static IConnectsToOrFinishes<INumberValidation<decimal>> IsNotNull(this decimal? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new NumberValidation<decimal>(subject, source, Validation.EnsureNotNull(subject, source));
}