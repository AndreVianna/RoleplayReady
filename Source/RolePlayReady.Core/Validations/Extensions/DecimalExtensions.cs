namespace System.Validations.Extensions;

public static class DecimalExtensions {
    public static INumberValidations<decimal> IsNullOr(this decimal? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new NumberValidations<decimal>(subject, source!);
    public static IConnects<INumberValidations<decimal>> IsNotNull(this decimal? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new Connects<INumberValidations<decimal>>(new NumberValidations<decimal>(subject, source!, Validation.EnsureNotNull(subject, source!)));
}