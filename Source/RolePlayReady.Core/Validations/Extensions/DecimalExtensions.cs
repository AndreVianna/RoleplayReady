namespace System.Validations.Extensions;

public static class DecimalExtensions {
    public static IDecimalValidation Is(this decimal subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new DecimalValidation(subject, source);
    public static IDecimalValidation IsNullOr(this decimal? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new DecimalValidation(subject, source);
    public static IConnectsToOrFinishes<IDecimalValidation> IsNotNull(this decimal? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new DecimalValidation(subject, source, Validation.EnsureNotNull(subject, source));
}