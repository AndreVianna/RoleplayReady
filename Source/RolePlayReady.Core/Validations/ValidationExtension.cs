namespace System.Validations;

public static class ValidationExtension {
    public static StringValidator Is(this string? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new(subject, source);
    public static StringsValidator ListIs(this IList<string> subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new(subject, source);
    public static IStringsConnectors ItemsAre(this IList<string> subject, Func<StringValidator, IStringConnectors> validate, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new StringsValidator(subject, source).ItemsAre(validate);
}