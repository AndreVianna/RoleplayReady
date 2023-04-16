namespace System.Validations.Extensions;

public static class StringExtension {
    public static IStringValidation Is(this string? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new StringValidation(subject, source);
}