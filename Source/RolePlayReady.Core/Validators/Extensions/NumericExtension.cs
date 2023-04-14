namespace System.Validators.Extensions;

public static class NumericExtension {
    public static INumericChecks ValueIs(this int subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new NumericValidator<int>(subject, source);

    public static INumericChecks ValueIs(this int? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new NumericValidator<int?>(subject, source);

    public static INumericChecks ValueIs(this decimal subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new NumericValidator<decimal>(subject, source);

    public static INumericChecks ValueIs(this decimal? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new NumericValidator<decimal?>(subject, source);
}