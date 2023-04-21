namespace System.Validations.Extensions;

public static class StringExtensions {
    public static ITextValidators IsNullOr(this string? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new TextValidation(subject, source);
    public static IConnectsToOrFinishes<ITextValidators> IsNotNull(this string? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new TextValidation(subject, source, Validation.EnsureNotNull(subject, source));
}