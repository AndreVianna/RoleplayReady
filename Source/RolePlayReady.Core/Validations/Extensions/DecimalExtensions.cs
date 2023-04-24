namespace System.Validations.Extensions;

public static class DecimalExtensions {
    public static INumberValidators<decimal> IsNullOr(this decimal? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new NumberValidation<decimal>(subject, source);
    public static IConnectsToOrFinishes<INumberValidators<decimal>> IsNotNull(this decimal? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new NumberValidation<decimal>(subject, source, Validation.EnsureNotNull(subject, source));
}