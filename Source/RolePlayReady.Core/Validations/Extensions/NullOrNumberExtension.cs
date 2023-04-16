namespace System.Validations.Extensions;

public static class NullOrNumberExtension {
    public static INullOrIntegerValidations ValueIs(this int? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => new NullOrIntegerValidation(subject, source);
}