namespace System.Validations.Extensions;

public static class StringExtension {
    public static IStringValidations ValueIs(this string? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new StringValidation(subject, source);
}