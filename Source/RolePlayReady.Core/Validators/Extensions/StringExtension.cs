namespace System.Validators.Extensions;

public static class StringExtension {
    public static IStringChecks ValueIs(this string? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new StringValidator(subject, source);
}