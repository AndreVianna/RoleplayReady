namespace System.Validations.Extensions;

public static class StringExtensions {
    public static IValidationsConnector<string?, TextValidations> IsOptional(this string? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => TextValidations.CreateAsOptional(subject, source!).AsConnection<string?, TextValidations>();
    public static IValidationsConnector<string?, TextValidations> IsRequired(this string? subject, [CallerArgumentExpression(nameof(subject))] string? source = null)
        => TextValidations.CreateAsRequired(subject, source!).AsConnection<string?, TextValidations>();
}