namespace System.Validations.Extensions;

public static class StringExtensions {
    public static IStringValidators IsNullOr(this string? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new StringValidation(subject, source);
    public static IConnectsToOrFinishes<IStringValidators> IsNotNull(this string? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new StringValidation(subject, source, Validation.EnsureNotNull(subject, source));
}