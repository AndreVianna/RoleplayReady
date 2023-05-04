namespace System.Validations.Extensions;

public static class StringExtensions {
    public static ITextValidations IsNullOr(this string? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new TextValidations(subject, source!);
    public static IConnects<ITextValidations> IsNotNull(this string? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new Connects<ITextValidations>(new TextValidations(subject, source!, Validation.EnsureNotNull(subject, source!)));
}